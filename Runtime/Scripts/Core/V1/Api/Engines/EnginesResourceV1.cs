using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace OpenAi.Api.V1
{
    public class EnginesResource : AApiResource<OpenAiApiV1>
    {
        public override string Endpoint => "/engines";

        public EnginesResource(OpenAiApiV1 parent) : base(parent) { }

        public EngineResource Engine(string engineId) => new EngineResource(this, engineId);


        public async Task<ApiResult<EnginesListV1>> ListAsync() => await GetAsync<EnginesListV1>();

        public Coroutine ListCoroutine(MonoBehaviour mono, Action<ApiResult<EnginesListV1>> onResult) => GetCoroutine(mono, onResult);

    }
}