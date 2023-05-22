using OpenAi.Json;

using System;
using UnityEngine;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// Part of completion response, count of i/o tokens
    /// </summary>
    [Serializable]
    public class UsageV1 : AModelV1
    {
        /// <summary>
        /// Prompt (input) token count
        /// </summary>
        public int prompt_tokens;

        /// <summary>
        /// Completion (output) token count
        /// </summary>
        public int completion_tokens;

        /// <summary>
        /// Total (i/o) token count
        /// </summary>
        public int total_tokens;

        /// <inheritdoc />
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            jb.Add(nameof(prompt_tokens), prompt_tokens);
            jb.Add(nameof(completion_tokens), completion_tokens);
            jb.Add(nameof(total_tokens), total_tokens);
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
                    case nameof(prompt_tokens):
                        prompt_tokens = int.Parse(jo.StringValue);
                        break;
                    case nameof(completion_tokens):
                        completion_tokens = int.Parse(jo.StringValue);
                        break;
                    case nameof(total_tokens):
                        total_tokens = int.Parse(jo.StringValue);
                        break;
                    default:
                        Debug.LogWarning("UsageV1: missing field " + jo.Name);
                        break;
                }
            }
        }

        public static implicit operator UsageV1(JsonObject v)
        {
            throw new NotImplementedException();
        }
    }
}