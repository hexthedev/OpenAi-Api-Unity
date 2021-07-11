using System.Text;

using UnityEngine.Networking;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// Represents an API endpoint
    /// </summary>
    public interface IApiResource
    {
        /// <summary>
        /// The parent resource object. Null if root.
        /// </summary>
        IApiResource ParentResource { get; }

        /// <summary>
        /// The url endpoint of the resource.
        /// </summary>
        string Endpoint { get; }

        /// <summary>
        /// The full constucted url to the endpoint
        /// </summary>
        string Url { get; }

        /// <summary>
        /// The endpoint is constructed by passing a <see cref="StringBuilder"/> up
        /// the tree until the parent is reached. The Parent then adds it's portion
        /// of the endpoint. The first child contributes it's portion, and so on. Until
        /// the whole endpoint is created.
        /// </summary>
        void ConstructEndpoint(StringBuilder sb);

        /// <summary>
        /// Populates a <see cref="UnityWebRequest"/> with the appropriate auth headers
        /// </summary>
        /// <returns></returns>
        void PopulateAuthHeaders(UnityWebRequest client);
    }
}