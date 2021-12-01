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
    /// An api resource represents some api endpoint with a specific function. An 
    /// example of an api resource is  https://api.openai.com/v1/engines. Each resource
    /// endpoint can have different functionity based on the HTTP method used (GET, POST, etc.)
    /// and the parameters provided in the request body. 
    /// </summary>
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
        /// Create a resource with a parent. Depending on how the api is
        /// architected, parents can provide common pieces of the api endpoints
        /// to their children. For example, https://api.openai.com/v1 could be
        /// represented by <see cref="OpenAiApiV1"/> with a child of <see cref="EnginesResourceV1"/>
        /// to represent https://api.openai.com/v1/engines
        /// </summary>
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
        protected async Task<ApiResult<TResponse>> GetAsync<TResponse>()
            where TResponse : AModelV1, new()
        {
            UnityWebRequest response = await GetRequestAsync();
            return PackResult<TResponse>(response);
        }

        /// <summary>
        /// Implements a get request as a Coroutine
        /// </summary>
        protected Coroutine GetCoroutine<TResponse>(MonoBehaviour mono, Action<ApiResult<TResponse>> onResult)
            where TResponse : AModelV1, new()
        {
            return mono.StartCoroutine(GetRoutine());

            IEnumerator GetRoutine()
            {
                UnityWebRequest response = null;
                yield return mono.StartCoroutine(GetRequestCoroutine((res) => response = res));
                if (response == null) onResult(new ApiResult<TResponse>() { IsSuccess = false });

                ApiResult<TResponse> result = PackResult<TResponse>(response);

                if (result == null) onResult(new ApiResult<TResponse>() { IsSuccess = false });
                else onResult(result);
            }
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Implements an async get request
        /// </summary>
        protected async Task<ApiResult> DeleteAsync()
        {
            UnityWebRequest response = await DeleteRequestAsync();
            return PackResult_RequestOnly(response);
        }

        /// <summary>
        /// Implements a get request as a Coroutine
        /// </summary>
        protected Coroutine DeleteCoroutine(MonoBehaviour mono, Action<ApiResult> onResult)
        {
            return mono.StartCoroutine(DeleteRoutine());

            IEnumerator DeleteRoutine()
            {
                UnityWebRequest response = null;
                yield return mono.StartCoroutine(DeleteRequestCoroutine((res) => response = res));
                if (response == null) onResult(new ApiResult() { IsSuccess = false });

                ApiResult result = PackResult_RequestOnly(response);

                if (result == null) onResult(new ApiResult() { IsSuccess = false });
                else onResult(result);
            }
        }
        #endregion

        #region POST
        /// <summary>
        /// Implements an async post request
        /// </summary>
        protected async Task<ApiResult<TResponse>> PostAsync<TRequest, TResponse>(TRequest request)
            where TRequest : AModelV1, new()
            where TResponse : AModelV1, new()
        {
            UnityWebRequest response = await PostRequestAsync(request);
            return PackResult<TResponse>(response);
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

                ApiResult<TResponse> result = PackResult<TResponse>(response);
                if (result == null) onResult(new ApiResult<TResponse>() { IsSuccess = false });
                else onResult(result);
            }
        }
        #endregion

        #region POST Event Stream
        /// <summary>
        /// Implements an async post request, with the reception method as event streams <see href="https://developer.mozilla.org/en-US/docs/Web/API/Server-sent_events/Using_server-sent_events#Event_stream_format"/>
        /// </summary>
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
        /// Implements a post request as a coroutine, with the reception method as event streams <see href="https://developer.mozilla.org/en-US/docs/Web/API/Server-sent_events/Using_server-sent_events#Event_stream_format"/>
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
            client.uploadHandler.Dispose();
            return client;
        }

        private async Task<UnityWebRequest> GetRequestAsync()
        {
            UnityWebRequest client = UnityWebRequest.Get(Url);
            ParentResource.PopulateAuthHeaders(client);
            await client.SendWebRequest();
            return client;
        }

        private async Task<UnityWebRequest> DeleteRequestAsync()
        {
            UnityWebRequest client = UnityWebRequest.Delete(Url);
            ParentResource.PopulateAuthHeaders(client);
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

        private IEnumerator DeleteRequestCoroutine(Action<UnityWebRequest> onResponse)
        {
            Task<UnityWebRequest> responseTask = DeleteRequestAsync();
            while (!responseTask.IsCompleted) yield return new WaitForEndOfFrame();
            UnityWebRequest response = responseTask.Result;
            onResponse(response);
        }


        private ApiResult<TResponse> PackResult<TResponse>(UnityWebRequest response)
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

        private ApiResult PackResult_RequestOnly(UnityWebRequest response)
        {
            ApiResult result = new ApiResult()
            {
                IsSuccess = response.result == UnityWebRequest.Result.Success,
                HttpResponse = response
            };

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