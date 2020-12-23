using System.Net.Http;
using System.Text;

namespace OpenAiApi
{
    /// <summary>
    /// A resource
    /// </summary>
    /// <typeparam name="TParent"></typeparam>
    public abstract class AResource<TParent> : IResource
        where TParent : IResource
    {
        private StringBuilder _sb = new StringBuilder();

        /// <inheritdoc />
        public IResource ParentResource { get; }

        /// <inheritdoc />
        public abstract string Endpoint { get; }

        /// <inheritdoc />
        public string Url
        {
            get
            {
                _sb.Clear();
                ConstructEndpoint(_sb);
                return _sb.ToString();
            }
        }

        /// <summary>
        /// Create a resource with a parent
        /// </summary>
        /// <param name="parent"></param>
        public AResource(TParent parent)
        {
            ParentResource = parent;
        }

        /// <inheritdoc />
        public void ConstructEndpoint(StringBuilder sb)
        {
            ParentResource.ConstructEndpoint(sb);
            sb.Append(Endpoint);
        }

        public void PopulateAuthHeaders(HttpClient client) => ParentResource.PopulateAuthHeaders(client);
    }
}