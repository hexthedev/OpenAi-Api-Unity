using System.Threading.Tasks;

namespace OpenAiApi
{
    public class SearchResourceV1 : AResource<EngineResource>
    {
        public override string Endpoint => "/search";

        public SearchResourceV1(EngineResource parent) : base(parent) { }

        public async Task<SearchListV1> Search(SearchRequestV1 request) => await PostAsync<SearchListV1>(request.ToJson());
    }
}