using OpenAi.Api.V1;

using UnityEngine;

namespace OpenAi.Unity.V1
{
    /// <summary>
    /// Arguments used to create a completion with the <see cref="OpenAiCompleterV1"/>
    /// </summary>
    [CreateAssetMenu(fileName = "CompletionArgs", menuName = "OpenAi/Unity/V1/CompletionArgs")]
    public class SOCompletionArgsV1 : ScriptableObject
    {
        [Tooltip("The maximum number of tokens to generate. Requests can use up to 2048 tokens shared between prompt and completion. (One token is roughly 4 characters for normal English text)")]
        public int max_tokens = 32;

        [Tooltip("What sampling temperature to use. Higher values means the model will take more risks. Try 0.9 for more creative applications, and 0 (argmax sampling) for ones with a well-defined answer. We generally recommend altering this or top_p but not both.")]
        public float temperature = 0.7f;

        [Tooltip("An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. So 0.1 means only the tokens comprising the top 10% probability mass are considered. We generally recommend altering this or temperature but not both.")]
        public float top_p = 1;

        [Tooltip("Up to 4 sequences where the API will stop generating further tokens. The returned text will not contain the stop sequence.")]
        public string[] stop = new string[4];

        [Tooltip("Number between 0 and 1 that penalizes new tokens based on whether they appear in the text so far. Increases the model's likelihood to talk about new topics. https://beta.openai.com/docs/api-reference/parameter-details/>")]
        public float presences_penalty = 0;

        [Tooltip("Number between 0 and 1 that penalizes new tokens based on their existing frequency in the text so far. Decreases the model's likelihood to repeat the same line verbatim. https://beta.openai.com/docs/api-reference/parameter-details")]
        public float frequency_penalty = 0;

        public CompletionRequestV1 AsCompletionRequest()
        {
            return new CompletionRequestV1()
            {
                max_tokens = max_tokens,
                temperature = temperature,
                top_p = top_p,
                stop = stop,
                frequency_penalty = frequency_penalty
            };
        }
    }
}