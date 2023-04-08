using OpenAi.Json;

using System;
using UnityEngine;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// A single choice returned by the OpenAi Api chat completion endpoint
    /// </summary>
    public class ChatChoiceV1 : AModelV1
    {
        /// <summary>
        /// The returned message
        /// </summary>
        public MessageV1 message;

        /// <summary>
        /// A portion of the returned message
        /// </summary>
        public DeltaV1 delta;

        /// <summary>
        /// the index of the choice
        /// </summary>
        public int index;

        /// <summary>
        /// The reason the engine ended the completion
        /// </summary>
        public string finish_reason;

        /// <inheritdoc />
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            if (message != null) 
                jb.Add(nameof(message), message.ToJson());
            else if (delta != null) 
                jb.Add(nameof(delta), delta.ToJson());
            jb.Add(nameof(index), index);
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
                    case nameof(message):
                        message = new MessageV1();
                        message.FromJson(jo);
                        break;
                    case nameof(delta):
                        delta = new DeltaV1();
                        delta.FromJson(jo);
                        break;
                    case nameof(index):
                        index = int.Parse(jo.StringValue);
                        break;
                    case nameof(finish_reason):
                        finish_reason = jo.StringValue;
                        break;
                    default:
                        Debug.LogWarning("ChatChoiceV1: missing field " + jo.Name);
                        break;
                }
            }
        }
    }
}