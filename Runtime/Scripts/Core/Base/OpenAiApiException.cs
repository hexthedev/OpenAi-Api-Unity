using System;

using UnityEngine;
using Object = UnityEngine.Object;

namespace OpenAi.Api
{
    public class OpenAiApiException : Exception
    {
        public Object Context { get; private set; }

        public OpenAiApiException(string message, Object context = null,  Exception innerException = null) : base(message, innerException)
        {
            Context = context;
        }

        public void LogAsError()
        {
            if(Context != null)
            {
                Debug.LogError($"OpenAiApi Error: {Message}", Context);
            }
            else
            {
                Debug.LogError($"OpenAiApi Error: {Message}");
            }
        }
    }
}