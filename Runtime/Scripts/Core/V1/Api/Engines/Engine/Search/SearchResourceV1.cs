using System.Threading.Tasks;

namespace OpenAiApi
{
    public class SearchResourceV1 : AApiResource<EngineResource>
    {
        public override string Endpoint => "/search";

        public SearchResourceV1(EngineResource parent) : base(parent) { }

        public async Task<ApiResult<SearchListV1>> Search(SearchRequestV1 request) => await PostAsync<SearchRequestV1, SearchListV1>(request);
    }
}