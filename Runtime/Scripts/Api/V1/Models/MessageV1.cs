using OpenAi.Json;

using System;
using UnityEngine;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// A single message passed to the OpenAi Api chat completion endpoint
    /// </summary>
   [System.Serializable]
    public class MessageV1 : AModelV1
    {
        public enum MessageRole { system, user, assistant };

        /// <summary>
        /// the message object role
        /// </summary>
        public MessageRole role;

        /// <summary>
        /// the content of the message
        /// </summary>
        public string content;

        /// <inheritdoc />
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            jb.Add(nameof(role), role.ToString());
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
                        role = (MessageRole)Enum.Parse(typeof(MessageRole), jo.StringValue);
                        break;
                    case nameof(content):
                        content = jo.StringValue;
                        break;
                    default:
                        Debug.LogWarning("MessageV1: missing field " + jo.Name);
                        break;
                }
            }
        }
    }
}