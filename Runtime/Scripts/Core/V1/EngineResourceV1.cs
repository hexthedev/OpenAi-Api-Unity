using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

namespace OpenAiApi
{
    public class EngineResource : AResource<EnginesResource>
    {
        private string _endpoint;
        public override string Endpoint => _endpoint;
    
        public EngineResource(EnginesResource parent, string engineId) : base(parent)
        {
            _endpoint = $"/{engineId}";
        }
    }
}