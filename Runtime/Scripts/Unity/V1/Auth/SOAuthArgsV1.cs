using OpenAi.Api;
using OpenAi.Api.V1;
using OpenAi.Json;

using System;
using System.IO;
using System.Text;

using UnityEngine;

namespace OpenAi.Unity.V1
{
    /// <summary>
    /// The Authentication arguments required to authenticate an OpenAI Api request.
    /// </summary>
    /// <remarks>
    /// Projects pushed to public reposities should not use the String authentication type, as the private key will be exposed to the public. 
    /// </remarks>
    [CreateAssetMenu(fileName = "AuthArgsV1", menuName = "OpenAi/Unity/V1/AuthArgs")]
    public class SOAuthArgsV1 : ScriptableObject
    {
        /// <summary>
        /// The method by which the authentication and organization keys are supplied to the auth args.
        /// </summary>
        public EAuthProvisionMethod AuthType = EAuthProvisionMethod.LocalFile;

        /// <summary>
        /// The private key provided by OpenAi. You private api key can be found at <see href="https://beta.openai.com/docs/developer-quickstart/your-api-keys"/> if you have an account.
        /// </summary>
        public string PrivateApiKey;

        /// <summary>
        /// The organization id provided by OpenAi. This is optional. It is only required when a user belongs to multiple organizations and they want to specifiy the organization who's quota should be consumed. 
        /// </summary>
        public string Organization;

        /// <summary>
        /// Based on the <see cref="AuthType"> resolves and provides the <see cref="SAuthArgsV1"> instance
        /// </summary>
        /// <returns></returns>
        public SAuthArgsV1 ResolveAuth()
        {
            switch (AuthType) 
            {
                case EAuthProvisionMethod.LocalFile: return ResolveLocalFileAuthArgs();
                case EAuthProvisionMethod.String: return new SAuthArgsV1() { private_api_key = PrivateApiKey, organization = Organization };
            }

            throw new Exception("Failed to resolve AuthArgs");
        }

        private SAuthArgsV1 ResolveLocalFileAuthArgs()
        {
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string authPath = $"{userPath}/.openai/auth.json";
            FileInfo fi = new FileInfo(authPath);

            if (!fi.Exists) throw new OpenAiApiException($"No authentication file exists at {authPath}", this);

            string json = null;
            using (FileStream fs = fi.OpenRead())
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
                json = Encoding.UTF8.GetString(buffer);
            }

            JsonObject des = JsonDeserializer.FromJson(json);

            SAuthArgsV1 authArgs = new SAuthArgsV1();
            authArgs.FromJson(des);

            return authArgs;
        }

        /// <summary>
        /// Options for provisioning auth keys
        /// </summary>
        public enum EAuthProvisionMethod
        {
            /// <summary>
            /// The local file looks for a auth.json file located at `~/.openai/auth.json` (Linux/Mac)
            /// or `%USERPROFILE%/.openai/auth.json` (Windows) and extracts the key.
            /// </summary>
            LocalFile = 0,

            /// <summary>
            /// The secret is copied into a field of this scriptable object and used directly
            /// </summary>
            String = 1
        }
    }
}