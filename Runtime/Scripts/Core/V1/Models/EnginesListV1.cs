using OpenAi.Json;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// A list of engines. <see cref="https://beta.openai.com/docs/api-reference/list-engines"/>
    /// </summary>
    public class EnginesListV1 : AModelV1
    {
        /// <summary>
        /// The list of engines
        /// </summary>
        public EngineV1[] data;

        /// <summary>
        /// The obj type (list)
        /// </summary>
        public string obj;

        /// <inheritdoc/>
        public override void FromJson(JsonObject json)
        {
            foreach (JsonObject jo in json.NestedValues)
            {
                switch (jo.Name)
                {
                    case "object":
                        obj = jo.StringValue;
                        break;
                    case "data":
                        data = ArrayFromJson<EngineV1>(jo);
                        break;
                }
            }
        }

        /// <inheritdoc/>
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            jb.AddArray(nameof(data), data);
            jb.Add("object", obj);
            jb.EndObject();

            return jb.ToString();
        }
    }
}
