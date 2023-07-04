using OpenAi.Api.V1;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace OpenAi.Unity.V1
{
    public class OpenAiImagesGenerationsV1 : AMonoSingleton<OpenAiImagesGenerationsV1>
    {
        OpenAiApiGatewayV1 _gateway = null;
        ImagesGenerationsResourceV1 _imagesGenerations = null;
        /// <summary>
        /// The auth arguments used to authenticate the api. Should not be changed after initalization. Once the <see cref="Api"/> is initalized it must be cleared and initialized again if any changes are made to this property
        /// </summary>
        [Tooltip("Arguments used to authenticate the OpenAi Api")]
        public SOAuthArgsV1 Auth;

        /// <summary>
        /// Arguments to configure the images generation
        /// </summary>
        [Tooltip("Arguments to configure the images generation")]
        public SOImagesGenerationsArgsV1 Args;

        /// <summary>
        /// Images generation result/s - list of urls/b64_json based on config
        /// </summary>
        [Tooltip("Images generation result/s - list of urls/b64_json based on config")]
        public List<string> generations;

        public void Start()
        {
            _gateway = OpenAiApiGatewayV1.Instance;

            if (Auth == null) Auth = ScriptableObject.CreateInstance<SOAuthArgsV1>();
            if (Args == null) Args = ScriptableObject.CreateInstance<SOImagesGenerationsArgsV1>();

            if (!_gateway.IsInitialized)
            {
                _gateway.Auth = Auth;
                _gateway.InitializeApi();
            }

            _imagesGenerations = _gateway.Api.Images.Generations;
        }

        public Coroutine Generate(string prompt, Action<string[]> onResponse, Action<UnityWebRequest> onError)
        {
            var request = Args == null ?
               new ImagesGenerationsRequestV1() :
               Args.AsImagesGenerationsRequest();

            request.prompt = prompt;

            this.generations = new List<string>();

            return this._imagesGenerations.CreateImagesGenerationCoroutine(this, request, (r) => HandleResponse(r, onResponse, onError));
        }

        private void HandleResponse(ApiResult<ImagesGenerationsV1> result, Action<string[]> onResponse, Action<UnityWebRequest> onError)
        {
            if (result.IsSuccess)
            {
                foreach (var gen in result.Result.data)
                {
                    switch (this.Args.imageFormat)
                    {
                        case ExtensionMethods.IMAGE_RESPONSE_FORMAT.url:
                            this.generations.Add(gen.url);
                            break;
                        case ExtensionMethods.IMAGE_RESPONSE_FORMAT.b64_json:
                            this.generations.Add(gen.b64_json);
                            break;
                    }
                }

                onResponse(this.generations.ToArray());
                return;
            }
            else
            {
                onError(result.HttpResponse);
                return;
            }
        }
    }
}