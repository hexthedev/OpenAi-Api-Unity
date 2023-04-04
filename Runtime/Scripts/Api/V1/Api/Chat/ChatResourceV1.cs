using System;
using System.Threading.Tasks;

using UnityEngine;

namespace OpenAi.Api.V1
{

    /// <summary>
    /// Resource providing base chat functionality. <see href="https://platform.openai.com/docs/api-reference/chat"/>
    /// </summary>
    public class ChatResourceV1 : AApiResource<OpenAiApiV1>
    {
        /// <inheritdoc/>
        public override string Endpoint => "/chat";

        /// <summary>
        /// Construct with parent
        /// </summary>
        /// <param name="parent"></param>
        public ChatResourceV1(OpenAiApiV1 parent) : base(parent) { Completions = new ChatCompletionsResourceV1(this); }

        /// <summary>
        /// Completions resource. <see href="https://platform.openai.com/docs/api-reference/chat/create"/>
        /// </summary>
        /// <param name="parent"></param>
        public ChatCompletionsResourceV1 Completions { get; private set; }
    }
}