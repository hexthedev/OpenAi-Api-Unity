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
        public IResource ParentResource { get; }

        /// <summary>
        /// The endpoint of the resource. For an api call www.api.com/resource, the Endpoint
        /// is /resource
        /// </summary>
        public abstract string Endpoint { get; }

        /// <summary>
        /// The full constucted url to the endpoint
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Construct to the endpoint by passing a string builder to parents
        /// </summary>
        /// <param name="sb"></param>
        public void ConstructEndpoint(StringBuilder sb);

        /// <summary>
        /// Populates a HttpClient with the appropriate auth headers
        /// </summary>
        /// <returns></returns>
        public void PopulateAuthHeaders(HttpClient client);

        /// <summary>
        /// Populates a HttpClient with the appropriate auth headers
        /// </summary>
        /// <returns></returns>
        public void PopulateAuthHeaders(HttpRequestMessage message);
    }
}