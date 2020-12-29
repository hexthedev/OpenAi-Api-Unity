using UnityEngine;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// The Authentication arguments required to authenticate an OpenAI Api request. This file should not be populated and exposed to the public, as the private key secret, and revealed to the public breaches the OpenAi terms and conditions. 
    /// </summary>
    [CreateAssetMenu(fileName = "AuthArgs", menuName = "OpenAi/Api/AuthArgs")]
    public class SOAuthArgs : ScriptableObject
    {
        /// <summary>
        /// The method by which the PrivateApiKey is found
        /// </summary>
        public EAuthType AuthType = EAuthType.LocalFile;

        /// <summary>
        /// The private key provided by OpenAi. You private api key can be found at <see href="https://beta.openai.com/docs/developer-quickstart/your-api-keys"/> if you have an account.
        /// </summary>
        public string PrivateApiKey;

        /// <summary>
        /// The organization id provided by OpenAi. This is optional. It is only required when a user belongs to multiple organizations and they want to specifiy the organization who's quota should be consumed. 
        /// </summary>
        public string Organization;

        /// <summary>
        /// Options for authenticating calls to the OpenAi Api
        /// </summary>
        public enum EAuthType
        {
            /// <summary>
            /// The local file looks for a key.txt file located at `~/.openai/key.txt` (Linux/Mac)
            /// or `%USERPROFILE%/.openai/key.txt` (Windows) and extracts the key.
            /// </summary>
            LocalFile = 0,

            /// <summary>
            /// The secret is copied into a field of this scriptable object and used directly
            /// </summary>
            String = 1
        }
    }
}