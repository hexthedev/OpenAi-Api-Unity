using OpenAi.Api;
using OpenAi.Api.V1;
using System.Collections.Generic;
using UnityEngine;

namespace OpenAi.Unity.V1
{
    /// <summary>
    /// Arguments used to create a chat completion with the <see cref="OpenAiChatCompleterV1"/>
    /// </summary>
    [CreateAssetMenu(fileName = "ChatCompletionArgs", menuName = "OpenAi/Unity/V1/ChatCompletionArgs")]
    public class SOChatCompletionArgsV1 : ScriptableObject
    {
        [Tooltip("What sampling temperature to use, between 0 and 2. Higher values like 0.8 will make the output more random, while lower values like 0.2 will make it more focused and deterministic. We generally recommend altering this or top_p but not both. ")]
        public float temperature = 1;

        [Tooltip("An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. So 0.1 means only the tokens comprising the top 10% probability mass are considered. We generally recommend altering this or temperature but not both. ")]
        public float top_p = 1;

        [Tooltip("How many chat completion choices to generate for each input message.")]
        public int n = 1;

        [Tooltip("If set, partial message deltas will be sent, like in ChatGPT. Tokens will be sent as data-only server-sent events as they become available, with the stream terminated by a data: [DONE] message. See the OpenAI Cookbook for example code.")]
        bool stream = false;

        [Tooltip("Up to 4 sequences where the API will stop generating further tokens. The returned text will not contain the stop sequence.")]
        public string[] stop = new string[4];

        [Tooltip("The maximum number of tokens to generate in the chat completion. The total length of input tokens and generated tokens is limited by the model's context length.")]
        public int max_tokens = 4097;

        [Tooltip("Number between -2.0 and 2.0. Positive values penalize new tokens based on whether they appear in the text so far, increasing the model's likelihood to talk about new topics. ")]
        public float presence_penalty = 0;

        [Tooltip("Number between -2.0 and 2.0. Positive values penalize new tokens based on their existing frequency in the text so far, decreasing the model's likelihood to repeat the same line verbatim.")]
        public float frequency_penalty = 0;

        [Tooltip("Modify the likelihood of specified tokens appearing in the completion. Accepts a json object that maps tokens(specified by their token ID in the GPT tokenizer) to an associated bias value from - 100 to 100.")]
        public Dictionary<string, int> logit_bias = new Dictionary<string, int>();

        [Tooltip("A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.")]
        public string user = "";

        public ChatCompletionRequestV1 AsChatCompletionRequest()
        {
            return new ChatCompletionRequestV1()
            {
                temperature = temperature,
                top_p = top_p,
                n = n,
                stream = stream,
                //stop = stop,
                max_tokens = max_tokens,
                presence_penalty = presence_penalty,
                frequency_penalty = frequency_penalty,
                // logit_bias = logit_bias,
                user = user
            };
        }
    }
}
