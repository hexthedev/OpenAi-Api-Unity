using OpenAi.Json;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// Info about an engine. <see cref="https://beta.openai.com/docs/api-reference/list-engines"/>
    /// </summary>
    public class EngineV1 : AModelV1
    {
        /// <summary>
        /// Engine id
        /// </summary>
        public string id;

        /// <summary>
        /// object type ("engine")
        /// </summary>
        public string obj;

        /// <summary>
        /// Owner of the engine
        /// </summary>
        public string owner;

        /// <summary>
        /// Is the engine ready? Not clear what this means
        /// </summary>
        public bool? ready;

        /// <inheritdoc />
        public override void FromJson(JsonObject json)
        {
            foreach(JsonObject jo in json.NestedValues)
            {
                switch (jo.Name)
                {
                    case "id":
                        id = jo.StringValue;
                        break;
                    case "object":
                        obj = jo.StringValue;
                        break;
                    case "owner":
                        owner = jo.StringValue;
                        break;
                    case "ready":
                        ready = bool.Parse(jo.StringValue);
                        break;
                }
            }
        }

        /// <inheritdoc />
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            jb.Add(nameof(id), id);
            jb.Add("object", obj);
            jb.Add(nameof(owner), owner);
            jb.Add(nameof(ready), ready);
            jb.EndObject();

            return jb.ToString();
        }
    }
}
