using OpenAi.Json;

using System;
using UnityEngine;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// The response to images generations request
    /// </summary>
    public class ImagesGenerationsV1 : AModelV1
    {
        /// <summary>
        /// The created time as Unix epoch
        /// </summary>
        public int created;

        public ImagesGenerationsDataV1[] data;

        /// <inheritdoc />
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            jb.Add(nameof(this.created), this.created);
            jb.AddArray(nameof(this.data), this.data);
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
                    case nameof(created):
                        created = int.Parse(jo.StringValue);
                        break;
                    case nameof(this.data):
                        var dataArray = new ImagesGenerationsDataV1[jo.NestedValues.Count];
                        for (int i = 0; i < dataArray.Length; i++)
                        {
                            var n = new ImagesGenerationsDataV1();
                            n.FromJson(jo.NestedValues[i]);
                            dataArray[i] = n;
                        }
                        this.data = dataArray;
                        break;
                    default:
                        Debug.LogWarning("ImagesGenerationsV1: missing field " + jo.Name);
                        break;
                }
            }
        }
    }
}