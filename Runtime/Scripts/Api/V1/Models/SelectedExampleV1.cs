using OpenAi.Json;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// <see cref="https://beta.openai.com/docs/api-reference/classifications/create#classifications/create-examples"/>
    /// </summary>
    public class SelectedExampleV1 : AModelV1
    {
        /// <summary>
        /// The document the label is associated to
        /// </summary>
        public string document;

        /// <summary>
        /// The label that is used when trying to classify a prompt against an example
        /// </summary>
        public string label;

        /// <summary>
        /// The example that you are performing a search on
        /// </summary>
        public string example;

        /// <inheritdoc/>
        public override void FromJson(JsonObject json)
        {
            foreach (JsonObject jo in json.NestedValues)
            {
                switch (jo.Name)
                {
                    case nameof(document):
                        document = jo.StringValue;
                        break;
                    case nameof(label):
                        label = jo.StringValue;
                        break;
                    case nameof(example):
                        example = jo.StringValue;
                        break;
                }
            }
        }

        /// <inheritdoc/>
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            jb.Add(nameof(document), document);
            jb.Add(nameof(label), label);
            jb.Add(nameof(example), example);
            jb.EndObject();

            return jb.ToString();
        }
    }
}