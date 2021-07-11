using OpenAi.Json;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenAi.Api.V1
{
    public class ClassificationV1 : AModelV1
    {
        /// <summary>
        /// id of classification
        /// </summary>
        public string completion;

        /// <summary>
        /// label selected by classificaiton
        /// </summary>
        public string label;

        /// <summary>
        /// model used to perform classification
        /// </summary>
        public string model;

        /// <summary>
        /// The type of task performed by the completion, in this case classification
        /// </summary>
        public string obj;

        /// <summary>
        /// The search model applied to the classification. 
        /// </summary>
        public string search_model;

        /// <summary>
        /// The examples provided to perform the completion with
        /// </summary>
        public SelectedExampleV1[] selected_examples;

        /// <inheritdoc/>
        public override void FromJson(JsonObject json)
        {
            foreach (JsonObject jo in json.NestedValues)
            {
                switch (jo.Name)
                {
                    case nameof(completion):
                        completion = jo.StringValue;
                        break;
                    case nameof(label):
                        label = jo.StringValue;
                        break;
                    case nameof(model):
                        model = jo.StringValue;
                        break;
                    case "object":
                        obj = jo.StringValue;
                        break;
                    case nameof(search_model):
                        search_model = jo.StringValue;
                        break;
                    case nameof(selected_examples):
                        selected_examples = ArrayFromJson<SelectedExampleV1>(jo);
                        break;

                }
            }
        }

        /// <inheritdoc/>
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            jb.Add(nameof(completion), completion);
            jb.Add(nameof(label), label);
            jb.Add(nameof(model), model);
            jb.Add("object", obj);
            jb.Add(nameof(search_model), search_model);
            jb.AddArray(nameof(selected_examples), selected_examples);
            jb.EndObject();

            return jb.ToString();
        }
    }
}