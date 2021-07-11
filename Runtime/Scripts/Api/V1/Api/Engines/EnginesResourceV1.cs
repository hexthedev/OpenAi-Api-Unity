using System;
using System.Threading.Tasks;

using UnityEngine;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// Provides functions to get infomation about available engines
    /// </summary>
    public class EnginesResourceV1 : AApiResource<OpenAiApiV1>
    {
        /// <inheritdoc />
        public override string Endpoint => "/engines";

        /// <summary>
        /// Construct Engines resource with parent
        /// </summary>
        /// <param name="parent"></param>
        public EnginesResourceV1(OpenAiApiV1 parent) : base(parent) { }

        /// <summary>
        /// Construct an engine resource using an engine id. The engine id must be a valid engine.
        /// <see href="https://beta.openai.com/docs/api-reference/retrieve-engine"/>
        /// </summary>
        /// <param name="engineId">The ID of the engine to use for this request</param>
        public EngineResourceV1 Engine(string engineId) => new EngineResourceV1(this, engineId);

        /// <summary>
        /// Lists the currently available engines, and provides basic information about each one such as the owner and availability. <see href="https://beta.openai.com/docs/api-reference/list-engines"/>
        /// </summary>
        public async Task<ApiResult<EnginesListV1>> ListEnginesAsync() => await GetAsync<EnginesListV1>();

        /// <summary>
        /// Lists the currently available engines, and provides basic information about each one such as the owner and availability. <see href="https://beta.openai.com/docs/api-reference/list-engines"/>
        /// </summary>
        public Coroutine ListEnginesCoroutine(MonoBehaviour mono, Action<ApiResult<EnginesListV1>> onResult) => GetCoroutine(mono, onResult);
    }
}