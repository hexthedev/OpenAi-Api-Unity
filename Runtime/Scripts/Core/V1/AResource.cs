using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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





        /// <summary>
        /// Performs an asyncronous post request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<T> PostAsync<T>(string body) 
            where T : AModelV1, new()
        {
            HttpClient client = new HttpClient();
            ParentResource.PopulateAuthHeaders(client);

            StringContent stringContent = new StringContent(body, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(Url, stringContent);
            if (response.IsSuccessStatusCode)
            {
                string resultAsString = await response.Content.ReadAsStringAsync();

                JsonObject obj = JsonDeserializer.FromJson(resultAsString);
                T res = new T();
                res.FromJson(obj);

                return res;
            }
            else
            {
                throw new HttpRequestException("Error calling OpenAi API to get completion.  HTTP status code: " + response.StatusCode.ToString() + ". Request body: " + response);
            }
        }

        /// <summary>
        /// Performs an asyncronous get request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>()
            where T : AModelV1, new()
        {
            HttpClient client = new HttpClient();
            ParentResource.PopulateAuthHeaders(client);

            var response = await client.GetAsync(Url);
            if (response.IsSuccessStatusCode)
            {
                string resultAsString = await response.Content.ReadAsStringAsync();

                JsonObject obj = JsonDeserializer.FromJson(resultAsString);
                T res = new T();
                res.FromJson(obj);

                return res;
            }
            else
            {
                throw new HttpRequestException("Error calling OpenAi API to get completion.  HTTP status code: " + response.StatusCode.ToString() + ". Request body: " + response);
            }
        }





        public void PopulateAuthHeaders(HttpClient client) => ParentResource.PopulateAuthHeaders(client);

        public void PopulateAuthHeaders(HttpRequestMessage message) => ParentResource.PopulateAuthHeaders(message);
    }
}