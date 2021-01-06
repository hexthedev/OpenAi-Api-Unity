using OpenAi.Json;

using System;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// Authentication arguments used to authenticate each api call to OpenAI
    /// </summary>
    public struct SAuthArgsV1 : IJsonable
    {
        /// <summary>
        /// The private api key found at <see href="https://beta.openai.com/docs/developer-quickstart"/>
        /// </summary>
        public string private_api_key;

        /// <summary>
        /// The organization id, used by individuals in multiple organizations to determine the quota to use
        /// </summary>
        public string organization;

        /// <inheritdoc/>
        public void FromJson(JsonObject jsonObj)
        {
            if (jsonObj.Type != EJsonType.Object) throw new Exception("Must be an object");

            foreach (JsonObject jo in jsonObj.NestedValues)
            {
                switch (jo.Name)
                {
                    case nameof(private_api_key):
                        private_api_key = jo.StringValue;
                        break;
                    case nameof(organization):
                        organization = jo.StringValue;
                        break;
                }
            }
        }

        /// <inheritdoc/>
        public string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            jb.Add(nameof(private_api_key), private_api_key);
            jb.Add(nameof(organization), organization);
            jb.EndObject();

            return jb.ToString();
        }
    }
}