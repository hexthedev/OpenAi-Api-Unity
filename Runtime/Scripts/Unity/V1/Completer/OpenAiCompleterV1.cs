using OpenAi.Api;
using OpenAi.Api.V1;

using System;
using System.Net.Http;

using UnityEngine;

namespace OpenAi.Unity.V1
{
    /// <summary>
    /// Automatically handles setting up OpenAiApi for simple completions with 1 engine. Exposes a simple method to allow users to perform completions
    /// </summary>
    public class OpenAiCompleterV1 : AMonoSingleton<OpenAiCompleterV1>
    {
        OpenAiApiGatewayV1 _gateway = null;

        EngineResource _engine = null;

        /// <summary>
        /// The auth arguments used to authenticate the api. Should not be changed after initalization. Once the <see cref="Api"/> is initalized it must be cleared and initialized again if any changes are made to this property
        /// </summary>
        [Tooltip("Arguments used to authenticate the OpenAi Api")]
        public SOAuthArgsV1 Auth;

        /// <summary>
        /// Arguments used to configure the engine when sending a completion
        /// </summary>
        [Tooltip("Arguments used to configure the completion")]
        public SOCompletionArgsV1 Args;

        /// <summary>
        /// The id of the engine to use
        /// </summary>
        [Tooltip("The id of the engine to use")]
        public EEngineName Engine;

        public void Start()
        {
            _gateway = OpenAiApiGatewayV1.Instance;

            if (!_gateway.IsInitialized) 
            {
                _gateway.Auth = Auth;
                _gateway.InitializeApi();
            }

            _engine = _gateway.Api.Engines.Engine(UTEEngineName.GetEngineName(Engine));
        }

        public void Complete(string prompt, Action<string> onResponse, Action<HttpResponseMessage> onError)
        {
            CompletionRequestV1 request = Args ? 
                new CompletionRequestV1() { max_tokens = 64 } : 
                Args.AsCompletionRequest();

            request.prompt = prompt;
            _engine.Completions.CreateCompletionCoroutine(this, request, (r) => HandleResponse(r, onResponse, onError));
        }

        private void HandleResponse(ApiResult<CompletionV1> result, Action<string> onResponse, Action<HttpResponseMessage> onError)
        {
            if (result.IsSuccess)
            {
                onResponse(result.Result.choices[0].text);
            } 
            else
            {
                onError(result.HttpResponse);
            }

            onResponse(null);
        }
    }
}