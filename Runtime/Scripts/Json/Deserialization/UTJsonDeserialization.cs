using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenAi.Json
{
    public static class UTJsonDeserialization
    {
        public static string[] AsStringArray(this JsonObject json)
        {
            if (json.Type == EJsonType.List)
            {
                List<string> extract = new List<string>();
                foreach (JsonObject obj in json.NestedValues)
                {
                    extract.Add(obj.StringValue);
                }
                return extract.ToArray();
            }
            else
            {
                throw new OpenAiJsonException($"Attempted to deserialize json to string[] but json object type is not a list. JSON: {json.StringValue}");
            }

            return null;
        }
    }
}