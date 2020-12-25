using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace OpenAiApi
{
    public class EnginesResource : AResource<OpenAiApiV1>
    {
        public override string Endpoint => "/engines";

        public EnginesResource(OpenAiApiV1 parent) : base(parent) { }

        public EngineResource Engine(string engineId) => new EngineResource(this, engineId);


        public async Task<EnginesListV1> List() => await GetAsync<EnginesListV1>();
    }
}