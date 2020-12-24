using System;
using UnityEngine;

namespace OpenAiApi
{
    public class ChoiceModelV1 : AModelV1
    {
        public string text;
        public int index;
        public string logprobs;
        public string finish_reason;

        /// <inheritdoc />
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            jb.Add(nameof(text), text);
            jb.Add(nameof(index), index);
            jb.Add(nameof(logprobs), logprobs);
            jb.Add(nameof(finish_reason), finish_reason);
            jb.EndObject();

            return jb.ToString();
        }

        /// <inheritdoc />
        public override void FromJson(JsonObject jsonObj)
        {
            if (jsonObj.Type != EJsonType.Object) throw new Exception("Must be an object");

            foreach (JsonObject jo in jsonObj.NestedValues)
            {
                switch (jo.Name)
                {
                    case nameof(text):
                        text = jo.StringValue;
                        break;
                    case nameof(index):
                        index = int.Parse(jo.StringValue);
                        break;
                    case nameof(logprobs):
                        logprobs = jo.StringValue;
                        break;
                    case nameof(finish_reason):
                        finish_reason = jo.StringValue;
                        break;
                }
            }
        }
    }
}