using System.Net.Http;
using System.Text;

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
        /// Populate a string builder with the full endpoint by passing string builder to parent
        /// </summary>
        /// <param name="sb"></param>
        void ConstructEndpoint(StringBuilder sb);

        /// <summary>
        /// Populates a <see cref="HttpClient"/> with the appropriate auth headers
        /// </summary>
        /// <returns></returns>
        void PopulateAuthHeaders(HttpClient client);
    }
}