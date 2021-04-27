using OpenAi.Json;

using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// A resource
    /// </summary>
    /// <typeparam name="TParent"></typeparam>
    public abstract class AApiResource<TParent> : IApiResource
        where TParent : IApiResource
    {
        private StringBuilder _sb = new StringBuilder();

        /// <inheritdoc />
        public IApiResource ParentResource { get; }

        /// <inheritdoc />
        public abstract string Endpoint { get; }

        /// <inheritdoc />
        public void PopulateAuthHeaders(UnityWebRequest client) => ParentResource.PopulateAuthHeaders(client);

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
        public AApiResource(TParent parent)
        {
            ParentResource = parent;
        }

        /// <inheritdoc />
        public void ConstructEndpoint(StringBuilder sb)
        {
            ParentResource.ConstructEndpoint(sb);
            sb.Append(Endpoint);
        }

        #region GET
        /// <summary>
        /// Implements an async get request
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        protected async Task<ApiResult<TResponse>> GetAsync<TResponse>()
            where TResponse : AModelV1, new()
        {
            UnityWebRequest response = await GetRequestAsync();
            return await PackResultAsync<TResponse>(response);
        }

        /// <summary>
        /// Implements a get request as a Coroutine
        /// </summary>
        /// <returns></returns>
        protected Coroutine GetCoroutine<TResponse>(MonoBehaviour mono, Action<ApiResult<TResponse>> onResult)
            where TResponse : AModelV1, new()
        {
            return mono.StartCoroutine(GetRoutine());

            IEnumerator GetRoutine()
            {
                UnityWebRequest response = null;
                yield return mono.StartCoroutine(GetRequestCoroutine((res) => response = res));
                if (response == null) onResult(new ApiResult<TResponse>() { IsSuccess = false });


                ApiResult<TResponse> result = PackResponse<TResponse>(response);

                if (result == null) onResult(new ApiResult<TResponse>() { IsSuccess = false });
                else onResult(result);
            }
        }
        #endregion

        #region POST
        /// <summary>
        /// Implements a async post request
        /// </summary>
        /// <returns></returns>
        protected async Task<ApiResult<TResponse>> PostAsync<TRequest, TResponse>(TRequest request)
            where TRequest : AModelV1, new()
            where TResponse : AModelV1, new()
        {
            UnityWebRequest response = await PostRequestAsync(request);
            return await PackResultAsync<TResponse>(response);
        }

        /// <summary>
        /// Implements a post request as a coroutine
        /// </summary>
        /// <returns></returns>
        protected Coroutine PostCoroutine<TRequest, TResponse>(MonoBehaviour mono, TRequest request, Action<ApiResult<TResponse>> onResult)
            where TRequest : AModelV1, new()
            where TResponse : AModelV1, new()
        {
            return mono.StartCoroutine(PostRoutine());

            IEnumerator PostRoutine()
            {
                UnityWebRequest response = null;
                yield return mono.StartCoroutine(PostRequestCoroutine(request, (res) => response = res));
                if (response == null) onResult(new ApiResult<TResponse>() { IsSuccess = false });

                ApiResult<TResponse> result = PackResponse<TResponse>(response);
                if (result == null) onResult(new ApiResult<TResponse>() { IsSuccess = false });
                else onResult(result);
            }
        }
        #endregion

        #region POST Event Stream
        /// <summary>
        /// Implements an async post request, with the reception method as event streams <see href="https://developer.mozilla.org/en-US/docs/Web/API/Server-sent_events/Using_server-sent_events#Event_stream_format"/>
        /// </summary>
        /// <returns></returns>
        protected async Task PostAsync_EventStream<TRequest, TResponse>(TRequest request, Action<ApiResult<TResponse>> onRequestStatus, Action<int, TResponse> onPartialResult, Action onCompletion = null)
            where TRequest : AModelV1, new()
            where TResponse : AModelV1, new()
        {
            UnityWebRequest response = await PostRequestAsync(request);
            
            ApiResult<TResponse> status = new ApiResult<TResponse>() { IsSuccess = response.result == UnityWebRequest.Result.Success, HttpResponse = response };
            onRequestStatus(status);
            
            if (response.result == UnityWebRequest.Result.Success) await ReadEventStreamAsync(response, onPartialResult, onCompletion);
        }

        /// <summary>
        /// Implements an post request as a coroutine, with the reception method as event streams <see href="https://developer.mozilla.org/en-US/docs/Web/API/Server-sent_events/Using_server-sent_events#Event_stream_format"/>
        /// </summary>
        /// <returns></returns>
        protected Coroutine PostCoroutine_EventStream<TRequest, TResponse>(MonoBehaviour mono, TRequest request, Action<ApiResult<TResponse>> onRequestStatus, Action<int, TResponse> onPartialResult, Action onCompletion = null)
            where TRequest : AModelV1, new()
            where TResponse : AModelV1, new()
        {
            return mono.StartCoroutine(PostEventStreamRoutine());

            IEnumerator PostEventStreamRoutine()
            {
                UnityWebRequest response = null;
                yield return mono.StartCoroutine(PostRequestCoroutine(request, (res) => response = res));

                if (response == null) onRequestStatus(new ApiResult<TResponse>() { IsSuccess = false });
                else onRequestStatus(new ApiResult<TResponse>() { IsSuccess = response.result == UnityWebRequest.Result.Success, HttpResponse = response });

                if (response != null && response.result == UnityWebRequest.Result.Success)
                {
                    Task ReadStreamTask = ReadEventStreamAsync(response, onPartialResult, onCompletion);
                    while (!ReadStreamTask.IsCompleted) yield return new WaitForEndOfFrame();
                }
            }
        }
        #endregion
        private async Task<UnityWebRequest> PostRequestAsync<TRequest>(TRequest request)
            where TRequest : AModelV1, new()
        {
            UnityWebRequest client = UnityWebRequest.Post(Url, string.Empty);
            ParentResource.PopulateAuthHeaders(client);

            AddJsonToUnityWebRequest(client, request.ToJson());

            await client.SendWebRequest();

            return client;
        }

        private async Task<UnityWebRequest> GetRequestAsync()
        {
            UnityWebRequest client = UnityWebRequest.Get(Url);
            await client.SendWebRequest();
            return client;
        }

        private async Task ReadEventStreamAsync<TResponse>(UnityWebRequest response, Action<int, TResponse> onPartialResult, Action onCompletion)
            where TResponse : AModelV1, new()
        {
            using (Stream stream = new MemoryStream(response.downloadHandler.data))
            {
                int index = 0;

                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        if (line.StartsWith("data: ")) line = line.Substring("data: ".Length);

                        if (line == "[DONE]")
                        {
                            if(onCompletion != null) onCompletion();
                            return;
                        }
                        else if (!string.IsNullOrWhiteSpace(line))
                        {
                            index++;
                            JsonObject obj = JsonDeserializer.FromJson(line.Trim());
                            TResponse streamedResult = new TResponse();
                            streamedResult.FromJson(obj);

                            onPartialResult(index, streamedResult);
                        }
                    }
                }
            }
        }
        private async Task<ApiResult<TResponse>> PackResultAsync<TResponse>(UnityWebRequest response)
            where TResponse : AModelV1, new()

        {
            ApiResult<TResponse> result = new ApiResult<TResponse>()
            {
                IsSuccess = response.result == UnityWebRequest.Result.Success,
                HttpResponse = response
            };

            if (result.IsSuccess)
            {
                string resultAsString = response.downloadHandler.text;
                result.Result = UnpackResponseObject<TResponse>(resultAsString);
            }

            return result;
        }

        private IEnumerator PostRequestCoroutine<TRequest>(TRequest request, Action<UnityWebRequest> onResponse)
            where TRequest : AModelV1, new()
        {
            Task<UnityWebRequest> responseTask = PostRequestAsync(request);
            while (!responseTask.IsCompleted) yield return new WaitForEndOfFrame();
            UnityWebRequest response = responseTask.Result;
            onResponse(response);
        }

        private IEnumerator GetRequestCoroutine(Action<UnityWebRequest> onResponse)
        {
            Task<UnityWebRequest> responseTask = GetRequestAsync();
            while (!responseTask.IsCompleted) yield return new WaitForEndOfFrame();
            UnityWebRequest response = responseTask.Result;
            onResponse(response);
        }

        private ApiResult<TResponse> PackResponse<TResponse>(UnityWebRequest response)
            where TResponse : AModelV1, new()
        {
            ApiResult<TResponse> result = new ApiResult<TResponse>()
            {
                IsSuccess = response.result == UnityWebRequest.Result.Success,
                HttpResponse = response
            };

            if (result.IsSuccess)
            {
                string resultAsString = response.downloadHandler.text;
                result.Result = UnpackResponseObject<TResponse>(resultAsString);
            }

            return result;
        }

        private void AddJsonToUnityWebRequest(UnityWebRequest client, string json)
        {
            client.SetRequestHeader("Content-Type", "application/json");
            client.uploadHandler = new UploadHandlerRaw(
                Encoding.UTF8.GetBytes(json)
            );
        }

        private TModel UnpackResponseObject<TModel>(string content)
            where TModel : AModelV1, new()
        {
            JsonObject obj = JsonDeserializer.FromJson(content);
            TModel res = new TModel();
            res.FromJson(obj);
            return res;
        }
    }
}