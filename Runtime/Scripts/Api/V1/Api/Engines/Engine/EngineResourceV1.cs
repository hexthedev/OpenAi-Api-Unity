using System;
using System.Threading.Tasks;

using UnityEngine;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// Resource related to a specific engine. <see href="https://beta.openai.com/docs/api-reference/retrieve-engine"/>
    /// </summary>
    public class EngineResourceV1 : AApiResource<EnginesResourceV1>
    {
        private string _endpoint;

        /// <inheritdoc />
        public override string Endpoint => _endpoint;

        /// <summary>
        /// Completions resource. <see href="https://beta.openai.com/docs/api-reference/create-completion"/> and onwards.
        /// </summary>
        public CompletionsResourceV1 Completions { get; private set; }

        /// <summary>
        /// Search resource. <see href="https://beta.openai.com/docs/api-reference/search"/>
        /// </summary>
        public SearchResourceV1 Search { get; private set; }

        /// <summary>
        /// Construct an engine resource with parent and engineId
        /// </summary>
        /// <param name="engineId">The ID of the engine to use for this request</param>
        public EngineResourceV1(EnginesResourceV1 parent, string engineId) : base(parent)
        {
            _endpoint = $"/{engineId}";
            Completions = new CompletionsResourceV1(this);
            Search = new SearchResourceV1(this);
        }

        /// <summary>
        /// Retrieves an engine instance, providing basic information about the engine such as the owner and availability. <see href="https://beta.openai.com/docs/api-reference/retrieve-engine"/>
        /// </summary>
        public async Task<ApiResult<EngineV1>> RetrieveEngineAsync() => await GetAsync<EngineV1>();

        /// <summary>
        /// Retrieves an engine instance, providing basic information about the engine such as the owner and availability. <see href="https://beta.openai.com/docs/api-reference/retrieve-engine"/>
        /// </summary>
        public Coroutine RetrieveEngineCoroutine(MonoBehaviour mono, Action<ApiResult<EngineV1>> onResult) => GetCoroutine(mono, onResult);
    }
}