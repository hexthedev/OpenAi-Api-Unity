using System;

using UnityEngine;
using Object = UnityEngine.Object;

namespace OpenAi.Api
{
    /// <summary>
    /// Exception that occurs when OpenAiApi calls fail
    /// </summary>
    public class OpenAiApiException : Exception
    {
        /// <summary>
        /// The context of the failed api call
        /// </summary>
        public Object Context { get; private set; }

        /// <summary>
        /// Construct with message, optional context and optional inner exception
        /// </summary>
        public OpenAiApiException(string message, Object context = null,  Exception innerException = null) : base(message, innerException)
        {
            Context = context;
        }

        /// <summary>
        /// Prints an error to the unity console.
        /// </summary>
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