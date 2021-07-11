using OpenAi.Json;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// <see cref="https://beta.openai.com/docs/api-reference/classifications/create#classifications/create-examples"/>
    /// </summary>
    public class LabeledExampleV1 : AModelV1
    {
        /// <summary>
        /// The example that you are performing a search on
        /// </summary>
        public string example;

        /// <summary>
        /// The label that is used when trying to classify a prompt against an example
        /// </summary>
        public string label;

        public LabeledExampleV1(string example, string label)
        {
            this.example = example == null ? "" : example;
            this.label = label == null ? "" : label;
        }

        public LabeledExampleV1() { }

        /// <inheritdoc/>
        public override void FromJson(JsonObject json)
        {
            if (json.NestedValues.Count != 2) 
                throw new OpenAiJsonException($"Received badly formated LabeledExampleV1 array");

            example = json.NestedValues[0].StringValue;
            label = json.NestedValues[1].StringValue;
        }

        /// <inheritdoc/>
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();
            jb.AddArray(new string[] { example, label });
            return jb.ToString();
        }
    }
}