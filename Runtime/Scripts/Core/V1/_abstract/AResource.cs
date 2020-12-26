using System;
using System.Collections;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace OpenAiApi
{
    /// <summary>
    /// A resource
    /// </summary>
    /// <typeparam name="TParent"></typeparam>
    public abstract class AResource<TParent> : IResource
        where TParent : IResource
    {
        private StringBuilder _sb = new StringBuilder();

        /// <inheritdoc />
        public IResource ParentResource { get; }

        /// <inheritdoc />
        public abstract string Endpoint { get; }

        /// <inheritdoc />
        public string Url
        {
            get
            {
                _sb.Clear();
                ConstructEndpoint(_sb);
                return _sb.ToString();
            }
        }

        /// <summary>
        /// Create a resource with a parent
        /// </summary>
        /// <param name="parent"></param>
        public AResource(TParent parent)
        {
            ParentResource = parent;
        }

        /// <inheritdoc />
        public void ConstructEndpoint(StringBuilder sb)
        {
            ParentResource.ConstructEndpoint(sb);
            sb.Append(Endpoint);
        }




        private HttpClient PrepareClient()
        {
            HttpClient client = new HttpClient();
            ParentResource.PopulateAuthHeaders(client);
            return client;
        }

        private async Task<HttpResponseMessage> Post<TRequest>(TRequest request)
            where TRequest : AModelV1, new()
        {
            HttpClient client = PrepareClient();
            StringContent stringContent = new StringContent(request.ToJson(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(Url, stringContent);
            return response;
        }

        private async Task<HttpResponseMessage> Get()
        {
            HttpClient client = PrepareClient();
            HttpResponseMessage response = await client.GetAsync(Url);
            return response;
        }

        private TModel UnpackResponseObject<TModel>(string content)
            where TModel : AModelV1, new()
        {
            JsonObject obj = JsonDeserializer.FromJson(content);
            TModel res = new TModel();
            res.FromJson(obj);
            return res;
        }

        /// <summary>
        /// Performs an asyncronous post request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request)
            where TRequest : AModelV1, new()
            where TResponse : AModelV1, new()
        {
            HttpResponseMessage response = await Post(request);

            if (response.IsSuccessStatusCode)
            {
                string resultAsString = await response.Content.ReadAsStringAsync();
                return UnpackResponseObject<TResponse>(resultAsString);
            }
            else
            {
                throw new HttpRequestException("Error calling OpenAi API to get completion.  HTTP status code: " + response.StatusCode.ToString() + ". Request body: " + response);
            }
        }



        public IEnumerator PostCoroutine<TRequest, TResponse>(TRequest request, Action<TResponse> callback)
            where TRequest : AModelV1, new()
            where TResponse : AModelV1, new()
        {
            Task<HttpResponseMessage> responseTask = Post(request);
            while (!responseTask.IsCompleted) yield return new WaitForEndOfFrame();
            
            HttpResponseMessage response = responseTask.Result;

            if (response.IsSuccessStatusCode)
            {
                Task<string> resultAsStringTask = response.Content.ReadAsStringAsync();
                while (!resultAsStringTask.IsCompleted) yield return new WaitForEndOfFrame();

                string resultAsString = resultAsStringTask.Result;
                callback(UnpackResponseObject<TResponse>(resultAsString));
            }
            else
            {
                throw new HttpRequestException("Error calling OpenAi API to get completion.  HTTP status code: " + response.StatusCode.ToString() + ". Request body: " + response);
            }
        } 





        /// <summary>
        /// PostAsync receiving a text stream in return. The text stream will stream back an entire object as the text is generated. 
        /// </summary>
        /// <param name="request">The request to send to the API.  This does not fall back to default values specified in <see cref="DefaultCompletionRequestArgs"/>.</param>
        /// <param name="resultHandler">An action to be called as each new result arrives, which includes the index of the result in the overall result set.</param>
        public async Task PostEventStreamAsync<TRequest, TResponse>(TRequest request, Action<int, TResponse> resultHandler)
            where TRequest : AModelV1, new()
            where TResponse : AModelV1, new()
        {
            await Task.Run(async () =>
            {
                HttpResponseMessage response = await Post(request);

                if (response.IsSuccessStatusCode)
                {
                    int index = 0;

                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string line;
                            while ((line = await reader.ReadLineAsync()) != null)
                            {
                                if (line.StartsWith("data: ")) line = line.Substring("data: ".Length);

                                if (line == "[DONE]")
                                {
                                    return;
                                }
                                else if (!string.IsNullOrWhiteSpace(line))
                                {
                                    index++;
                                    JsonObject obj = JsonDeserializer.FromJson(line.Trim());
                                    TResponse streamedResult = new TResponse();
                                    streamedResult.FromJson(obj);

                                    resultHandler(index, streamedResult);
                                }
                            }
                        }
                    }
                }
                else
                {
                    throw new HttpRequestException("Error calling OpenAi API to get completion.  HTTP status code: " + response.StatusCode.ToString());
                }
            });
        }

        /// <summary>
        /// Performs an asyncronous get request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TResponse> GetAsync<TResponse>()
            where TResponse : AModelV1, new()
        {
            HttpResponseMessage response = await Get();
            if (response.IsSuccessStatusCode)
            {
                string resultAsString = await response.Content.ReadAsStringAsync();
                return UnpackResponseObject<TResponse>(resultAsString);
            }
            else
            {
                throw new HttpRequestException("Error calling OpenAi API to get completion.  HTTP status code: " + response.StatusCode.ToString() + ". Request body: " + response);
            }
        }

        public void PopulateAuthHeaders(HttpClient client) => ParentResource.PopulateAuthHeaders(client);

        public void PopulateAuthHeaders(HttpRequestMessage message) => ParentResource.PopulateAuthHeaders(message);
    }
}