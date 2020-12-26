using System.Net.Http;
using System.Text;

namespace OpenAiApi
{
    /// <summary>
    /// Represents an API endpoint
    /// </summary>
    public interface IResource
    {
        /// <summary>
        /// The parent reosurce object.
        /// </summary>
        IResource ParentResource { get; }

        /// <summary>
        /// The endpoint of the resource. For an api call www.api.com/resource, the Endpoint
        /// is /resource
        /// </summary>
        string Endpoint { get; }

        /// <summary>
        /// The full constucted url to the endpoint
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Construct to the endpoint by passing a string builder to parents
        /// </summary>
        /// <param name="sb"></param>
        void ConstructEndpoint(StringBuilder sb);

        /// <summary>
        /// Populates a HttpClient with the appropriate auth headers
        /// </summary>
        /// <returns></returns>
        void PopulateAuthHeaders(HttpClient client);

        /// <summary>
        /// Populates a HttpClient with the appropriate auth headers
        /// </summary>
        /// <returns></returns>
        void PopulateAuthHeaders(HttpRequestMessage message);
    }
}