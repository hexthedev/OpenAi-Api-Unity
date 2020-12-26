using System;
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





        /// <summary>
        /// Performs an asyncronous post request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TResponse> PostAsync<TRequest, TResponse>(TRequest request)
            where TRequest : AModelV1, new()
            where TResponse : AModelV1, new()
        {
            HttpClient client = new HttpClient();
            ParentResource.PopulateAuthHeaders(client);

            StringContent stringContent = new StringContent(request.ToJson(), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(Url, stringContent);
            if (response.IsSuccessStatusCode)
            {
                string resultAsString = await response.Content.ReadAsStringAsync();

                JsonObject obj = JsonDeserializer.FromJson(resultAsString);
                TResponse res = new TResponse();
                res.FromJson(obj);

                return res;
            }
            else
            {
                throw new HttpRequestException("Error calling OpenAi API to get completion.  HTTP status code: " + response.StatusCode.ToString() + ". Request body: " + response);
            }
        }

        public void PostCoroutine<TRequest, TResponse>(TRequest request, MonoBehaviour monoProvider, Action<TResponse> callback)
        {

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
                HttpClient client = new HttpClient();
                ParentResource.PopulateAuthHeaders(client);

                StringContent stringContent = new StringContent(request.ToJson(), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(Url, stringContent);

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
                    throw new HttpRequestException("Error calling OpenAi API to get completion.  HTTP status code: " + response.StatusCode.ToString() + ". Request body: " + stringContent);
                }
            });
        }

        /// <summary>
        /// Performs an asyncronous get request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>()
            where T : AModelV1, new()
        {
            HttpClient client = new HttpClient();
            ParentResource.PopulateAuthHeaders(client);

            var response = await client.GetAsync(Url);
            if (response.IsSuccessStatusCode)
            {
                string resultAsString = await response.Content.ReadAsStringAsync();

                JsonObject obj = JsonDeserializer.FromJson(resultAsString);
                T res = new T();
                res.FromJson(obj);

                return res;
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