using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

namespace OpenAiApi
{
    public class EnginesResource : AResource<OpenAiApiV1>
    {
        public override string Endpoint => "/engine";

        public EnginesResource(OpenAiApiV1 parent) : base(parent) { }
    }
}