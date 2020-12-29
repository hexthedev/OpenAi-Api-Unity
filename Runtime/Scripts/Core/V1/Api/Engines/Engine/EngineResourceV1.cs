using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace OpenAi.Api.V1
{
    public class EngineResource : AApiResource<EnginesResource>
    {
        private string _endpoint;
        public override string Endpoint => _endpoint;

        public CompletionsResourceV1 Completions { get; private set; }

        public SearchResourceV1 Search { get; private set; }

        public EngineResource(EnginesResource parent, string engineId) : base(parent)
        {
            _endpoint = $"/{engineId}";
            Completions = new CompletionsResourceV1(this);
            Search = new SearchResourceV1(this);
        }

        public async Task<ApiResult<EngineV1>> RetrieveAsync() => await GetAsync<EngineV1>();

        public Coroutine RetrieveCoroutine(MonoBehaviour mono, Action<ApiResult<EngineV1>> onResult) => GetCoroutine(mono, onResult);
    }
}