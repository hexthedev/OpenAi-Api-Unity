using UnityEngine.Networking;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// A result of an api call
    /// </summary>
    /// <typeparam name="TResult">The type of result expected from the api call</typeparam>
    public class ApiResult<TResult>
    {
        /// <summary>
        /// True if the request status is success code
        /// </summary>
        public bool IsSuccess;

        /// <summary>
        /// The completed UnityWebRequest
        /// </summary>
        public UnityWebRequest HttpResponse;

        /// <summary>
        /// The deserailized response from the call. Null if no repsonse received or call unsuccessful.
        /// </summary>
        public TResult Result;
    }
}