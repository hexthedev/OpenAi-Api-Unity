using OpenAi.Json;

using System;
using UnityEngine;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// A partial message returned from the OpenAi Api chat completion endpoint
    /// </summary>
    public class DeltaV1 : AModelV1
    {
        /// <summary>
        /// the message object role
        /// </summary>
        public MessageV1.MessageRole? role;

        /// <summary>
        /// the content of the message
        /// </summary>
        public string content;

        /// <inheritdoc />
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            if (role != null) jb.Add(nameof(role), role.ToString());
            jb.Add(nameof(content), content);
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
                    case nameof(role):
                        role = (MessageV1.MessageRole?)Enum.Parse(typeof(MessageV1.MessageRole), jo.StringValue);
                        break;
                    case nameof(content):
                        content = jo.StringValue;
                        break;
                    default:
                        Debug.LogWarning("DeltaV1: missing field " + jo.Name);
                        break;
                }
            }
        }
    }
}
