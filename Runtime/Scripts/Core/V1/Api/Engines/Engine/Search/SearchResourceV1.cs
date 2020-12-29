using System;
using System.Threading.Tasks;

using UnityEngine;

namespace OpenAi.Api.V1
{
    public class SearchResourceV1 : AApiResource<EngineResource>
    {
        public override string Endpoint => "/search";

        public SearchResourceV1(EngineResource parent) : base(parent) { }

        public async Task<ApiResult<SearchListV1>> SearchAsync(SearchRequestV1 request) => await PostAsync<SearchRequestV1, SearchListV1>(request);
        public Coroutine SearchCoroutine(MonoBehaviour mono, SearchRequestV1 request, Action<ApiResult<SearchListV1>> onResult) => PostCoroutine(mono, request, onResult);
    }
}