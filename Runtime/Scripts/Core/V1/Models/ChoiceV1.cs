using OpenAi.Json;

using System;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// A single choice returned by the OpenAi Api completion endpoint
    /// </summary>
    public class ChoiceV1 : AModelV1
    {
        /// <summary>
        /// The returned text
        /// </summary>
        public string text;

        /// <summary>
        /// the index of the choice
        /// </summary>
        public int index;

        /// <summary>
        /// The log probabilities
        /// </summary>
        public string logprobs;

        /// <summary>
        /// The reason the engine ended the completion
        /// </summary>
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