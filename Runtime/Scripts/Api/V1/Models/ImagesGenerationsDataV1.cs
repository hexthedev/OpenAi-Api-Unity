using OpenAi.Json;

using System;
using UnityEngine;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// Data part of the response to images generations request
    /// </summary>
    public class ImagesGenerationsDataV1 : AModelV1
    {
        public string url;
        public string b64_json;

        /// <inheritdoc />
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            jb.Add(nameof(this.url), this.url);
            jb.Add(nameof(this.b64_json), this.b64_json);
            jb.EndObject();

            return jb.ToString();
        }

        /// <inheritdoc />
        public override void FromJson(JsonObject jsonObj)
        {
            if (jsonObj.Type != EJsonType.Object) throw new Exception("Must be an object");

            foreach(JsonObject jo in jsonObj.NestedValues)
            {
                switch (jo.Name)
                {
                    case nameof(this.url):
                        this.url = jo.StringValue;
                        break;
                    case nameof(this.b64_json):
                        this.b64_json = jo.StringValue;
                        break;
                    default:
                        Debug.LogWarning("ImagesGenerationsDataV1: missing field " + jo.Name);
                        break;
                }
            }
        }
    }
}
