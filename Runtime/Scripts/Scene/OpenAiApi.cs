using UnityEngine;

namespace OpenAi.Api.V1
{
    public class OpenAiApi : AMonoSingleton<OpenAiApi>
    {
        [SerializeField]
        [Tooltip("Arguments used to authenticate the OpenAi Api")]
        private SOAuthArgs _auth;

        public OpenAiApiV1 Api { get; private set; }

        void Start()
        {
            if (_auth == null)
            {
                Debug.LogError("OpenAi API Error: OpenAi Api cannot be authenticated. No SOAuthArgs provided. API won't be created");
            } 
            else
            {
                Api = new OpenAiApiV1(new SAuthArgs());
            }
        }

    }
}