using System;
using System.Threading.Tasks;

using UnityEngine;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// Resource for performing semantic searches over lists of documents <see href="https://beta.openai.com/docs/api-reference/search"/>
    /// </summary>
    public class SearchResourceV1 : AApiResource<EngineResourceV1>
    {
        /// <inheritdoc/>
        public override string Endpoint => "/search";

        /// <summary>
        /// Construct resource with parent
        /// </summary>
        /// <param name="parent"></param>
        public SearchResourceV1(EngineResourceV1 parent) : base(parent) { }

        /// <summary>
        /// Performs a semantic search over a list of documents. Response includes the list of scored documents (in the same order that they were passed in). The similarity score is a positive score that usually ranges from 0 to 300 (but can sometimes go higher), where a score above 200 usually means the document is semantically similar to the query. <see href="https://beta.openai.com/docs/api-reference/search"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SearchListV1>> SearchAsync(SearchRequestV1 request) => await PostAsync<SearchRequestV1, SearchListV1>(request);


        /// <summary>
        /// Performs a semantic search over a list of documents. Response includes the list of scored documents (in the same order that they were passed in). The similarity score is a positive score that usually ranges from 0 to 300 (but can sometimes go higher), where a score above 200 usually means the document is semantically similar to the query. <see href="https://beta.openai.com/docs/api-reference/search"/>
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="request"></param>
        /// <param name="onResult"></param>
        /// <returns></returns>
        public Coroutine SearchCoroutine(MonoBehaviour mono, SearchRequestV1 request, Action<ApiResult<SearchListV1>> onResult) => PostCoroutine(mono, request, onResult);
    }
}