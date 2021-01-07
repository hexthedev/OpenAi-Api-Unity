using OpenAi.Json;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// A returned value from a GPT-3 search.
    /// </summary>
    public class SearchV1 : AModelV1
    {
        /// <summary>
        /// The document searched
        /// </summary>
        public int document;
        
        /// <summary>
        /// The object type
        /// </summary>
        public string obj;

        /// <summary>
        /// The score attributed to the document
        /// </summary>
        public float score;

        /// <inheritdoc/>
        public override void FromJson(JsonObject json)
        {
            foreach(JsonObject jo in json.NestedValues)
            {
                switch (jo.Name)
                {
                    case nameof(document):
                        document = int.Parse(jo.StringValue);
                        break;
                    case "object":
                        obj = jo.StringValue;
                        break;
                    case nameof(score):
                        score = float.Parse(jo.StringValue);
                        break;
                }
            }
        }

        /// <inheritdoc/>
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            jb.Add(nameof(document), document);
            jb.Add("object", obj);
            jb.Add(nameof(score), score);
            jb.EndObject();

            return jb.ToString();
        }
    }
}