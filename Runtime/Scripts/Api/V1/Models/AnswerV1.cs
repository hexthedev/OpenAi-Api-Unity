using OpenAi.Json;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenAi.Api.V1
{
    public class AnswerV1 : AModelV1
    {
        public string[] answers;
        public string completion;
        public string model;
        public string obj;
        public string search_model;
        public SelectedDocumentV1[] selected_documents;

        public override void FromJson(JsonObject json)
        {
            foreach (JsonObject jo in json.NestedValues)
            {
                switch (jo.Name)
                {
                    case nameof(answers):
                        answers = jo.AsStringArray();
                        break;
                    case nameof(completion):
                        completion = jo.StringValue;
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
                    case nameof(selected_documents):
                        selected_documents = ArrayFromJson<SelectedDocumentV1>(jo);
                        break;

                }
            }
        }

        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            jb.AddArray(nameof(answers), answers);
            jb.Add(nameof(completion), completion);
            jb.Add(nameof(model), model);
            jb.Add("object", obj);
            jb.Add(nameof(search_model), search_model);
            jb.AddArray(nameof(selected_documents), selected_documents);
            jb.EndObject();

            return jb.ToString();
        }
    }
}