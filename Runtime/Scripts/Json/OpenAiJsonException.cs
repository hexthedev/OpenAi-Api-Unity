using System;
using UnityEngine;

namespace OpenAi.Json
{
    /// <summary>
    /// Exception thrown during json deserailization or serialization in the OpenAi Api Unity library
    /// </summary>
    public class OpenAiJsonException : Exception
    {
        /// <summary>
        /// Constrct with message and inner exception
        /// </summary>
        /// <param name="message">Message to show</param>
        /// <param name="innerException">Inner exception</param>
        public OpenAiJsonException(string message, Exception innerException = null) : base(message, innerException)
        {
        }

        /// <summary>
        /// Print to Unity Log as errors
        /// </summary>
        public void LogAsError()
        {
            Debug.LogError($"OpenAi Json Error: {Message}");
        }
    }
}