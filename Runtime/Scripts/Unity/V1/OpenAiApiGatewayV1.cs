using OpenAi.Api.V1;

using UnityEngine;

namespace OpenAi.Unity.V1
{
    /// <summary>
    /// A singleton that handles the inialization of an OpenAiApiV1 object and provides access to it. For the <see cref="Api"/> to function
    /// it needs to be initalized.
    /// </summary>
    public class OpenAiApiGatewayV1 : AMonoSingleton<OpenAiApiGatewayV1>
    {
        /// <summary>
        /// If true, calls <see cref="InitializeApi"/> in <see cref="Start"/>
        /// </summary>
        [Tooltip("If true, initalizes the api in Start. Otherwise requires initalization programmatically")]
        public bool InitializeOnStart = false;

        /// <summary>
        /// The auth arguments used to authenticate the api. Should not be changed after initalization. Once the <see cref="Api"/> is initalized it must be cleared and initialized again if any changes are made to this property
        /// </summary>
        [Tooltip("Arguments used to authenticate the OpenAi Api")]
        public SOAuthArgsV1 Auth;

        /// <summary>
        /// True if the <see cref="Api"/> has been initialized successfully and is ready for api calls to be made.
        /// </summary>
        public bool IsInitialized { get; private set; } = false;

        /// <summary>
        /// <see cref="OpenAiApiV1"/> instance used the make api calls through internal resources.
        /// </summary>
        public OpenAiApiV1 Api { get; private set; }

        void Start()
        {
            if (InitializeOnStart) InitializeApi();
        }

        /// <summary>
        /// Sets the <see cref="Api"/> by resolving <see cref="Auth"/>
        /// </summary>
        public void InitializeApi()
        {
            if (Auth == null)
            {
                Debug.LogError("OpenAi API Error: OpenAi Api cannot be authenticated. No SOAuthArgs provided. API won't be created");
            }
            else
            {
                Api = new OpenAiApiV1(Auth.ResolveAuth());
                IsInitialized = true;
            }
        }

        /// <summary>
        /// Sets the <see cref="Api"/> to null and marks <see cref="IsInitialized"/> false
        /// </summary>
        public void ClearApi()
        {
            Api = null;
            IsInitialized = false;
        }
    }
}