using OpenAi.Api;
using OpenAi.Api.V1;

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace OpenAi.Unity.V1
{
    /// <summary>
    /// Automatically handles setting up OpenAiApi for chat completions. Exposes a simple method to allow users to perform completions
    /// </summary>
    public class OpenAiChatCompleterV1 : AMonoSingleton<OpenAiChatCompleterV1>
    {
        OpenAiApiGatewayV1 _gateway = null;
        ChatCompletionsResourceV1 _model = null;

        /// <summary>
        /// The auth arguments used to authenticate the api. Should not be changed after initalization. Once the <see cref="Api"/> is initalized it must be cleared and initialized again if any changes are made to this property
        /// </summary>
        [Tooltip("Arguments used to authenticate the OpenAi Api")]
        public SOAuthArgsV1 Auth;

        /// <summary>
        /// Arguments used to configure the model when sending a chat completion
        /// </summary>
        [Tooltip("Arguments used to configure the chat completion")]
        public SOChatCompletionArgsV1 Args;

        /// <summary>
        /// The id of the model to use
        /// </summary>
        [Tooltip("The id of the model to use")]
        public EEngineName Model = EEngineName.gpt_35_turbo;

        /// <summary>
        /// Current model usage
        /// </summary>
        [Tooltip("Current model usage")]
        public UsageV1 Usage;

        /// <summary>
        /// The dialogue of chat messages, may be prepopulated
        /// </summary>
        [Tooltip("The dialogue of chat messages, may be prepopulated")]
        public List<MessageV1> dialogue;

        public void Start()
        {
            _gateway = OpenAiApiGatewayV1.Instance;

            if (Auth == null) Auth = ScriptableObject.CreateInstance<SOAuthArgsV1>();
            if (Args == null) Args = ScriptableObject.CreateInstance<SOChatCompletionArgsV1>();

            if (!_gateway.IsInitialized)
            {
                _gateway.Auth = Auth;
                _gateway.InitializeApi();
            }

            _model = _gateway.Api.Chat.Completions;
        }

        public Coroutine Complete(string prompt, Action<string> onResponse, Action<UnityWebRequest> onError)
        {
            MessageV1 message = new MessageV1();
            message.role = MessageV1.MessageRole.user;
            message.content = prompt;

            dialogue.Add(message);

            return Complete(onResponse, onError);
        }

        public Coroutine Complete(Action<string> onResponse, Action<UnityWebRequest> onError)
        {
            ChatCompletionRequestV1 request = Args == null ?
               new ChatCompletionRequestV1() :
               Args.AsChatCompletionRequest();

            request.model = UTEChatModelName.GetModelName(Model);
            request.messages = dialogue;

            return _model.CreateChatCompletionCoroutine(this, request, (r) => HandleResponse(r, onResponse, onError));
        }

        private void HandleResponse(ApiResult<ChatCompletionV1> result, Action<string> onResponse, Action<UnityWebRequest> onError)
        {
            if (result.IsSuccess)
            {
                foreach (ChatChoiceV1 choice in result.Result.choices)
                {
                    dialogue.Add(choice.message);
                }

                Usage = result.Result.usage;
                onResponse(dialogue[dialogue.Count - 1].content);
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