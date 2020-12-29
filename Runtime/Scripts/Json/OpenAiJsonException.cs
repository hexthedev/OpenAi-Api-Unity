using System;

using UnityEngine;
using Object = UnityEngine.Object;

namespace OpenAi.Api
{
    public class OpenAiJsonException : Exception
    {
        public OpenAiJsonException(string message, Exception innerException = null) : base(message, innerException)
        {
        }

        public void LogAsError()
        {
            Debug.LogError($"OpenAi Json Error: {Message}");
        }
    }
}