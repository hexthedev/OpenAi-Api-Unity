using OpenAi.Json;

using System;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// Object used when requesting a search. <see href="https://beta.openai.com/docs/api-reference/search"/>
    /// </summary>
    public class SearchRequestV1 : AModelV1
    {
        /// <summary>
        /// Up to 200 documents to search over, provided as a list of strings. The maximum document length(in tokens) is 2034 minus the number of tokens in the query.
        /// </summary>
        public string[] documents;

        /// <summary>
        /// Query to search against the documents.
        /// </summary>
        public string query;

        /// <inheritdoc/>
        public override void FromJson(JsonObject json)
        {
            if (json.Type != EJsonType.Object) throw new Exception("Must be an object");

            foreach (JsonObject jo in json.NestedValues)
            {
                switch (jo.Name)
                {
                    case nameof(documents):
                        documents = new string[jo.NestedValues.Count];
                        for(int i = 0; i<documents.Length; i++)
                        {
                            documents[i] = jo.NestedValues[i].StringValue;
                        }
                        break;
                    case nameof(query):
                        query = jo.StringValue;
                        break;
                }
            }
        }

        /// <inheritdoc/>
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();
            jb.StartObject();
            jb.AddArray(nameof(documents), documents);
            jb.Add(nameof(query), query);
            jb.EndObject();

            return jb.ToString();
        }
    }
}