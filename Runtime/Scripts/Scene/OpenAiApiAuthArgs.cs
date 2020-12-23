using UnityEngine;

namespace OpenAiApi
{
    /// <summary>
    /// The Authentication arguments required to authenticate an OpenAI Api request. This file should not be populated and exposed to the public, as the private key secret, and revealed to the public breaches the OpenAi terms and conditions. 
    /// </summary>
    [CreateAssetMenu(fileName = "OpenAiApiAuthArgs", menuName = "OpenAi/Api/AuthArgs")]
    public class OpenAiApiAuthArgs : ScriptableObject
    {
        /// <summary>
        /// The private key provided by OpenAi. You private api key can be found at <see href="https://beta.openai.com/docs/developer-quickstart/your-api-keys"/> if you have an account.
        /// </summary>
        public string PrivateApiKey;

        /// <summary>
        /// The organization id provided by OpenAi. This is optional. It is only required when a user belongs to multiple organizations and they want to specifiy the organization who's quota should be consumed. 
        /// </summary>
        public string Organization;
    }
}