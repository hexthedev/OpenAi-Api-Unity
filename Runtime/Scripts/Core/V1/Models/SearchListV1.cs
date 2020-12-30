using OpenAi.Json;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// A list of searchs to perform in a OpenAi Api search query
    /// </summary>
    public class SearchListV1 : AModelV1
    {
        /// <summary>
        /// The array of searches to perform
        /// </summary>
        public SearchV1[] data;

        /// <summary>
        /// The object type
        /// </summary>
        public string obj;

        /// <inheritdoc/>
        public override void FromJson(JsonObject json)
        {
            foreach(JsonObject jb in json.NestedValues)
            {
                switch (jb.Name) 
                {
                    case nameof(data):
                        data = new SearchV1[jb.NestedValues.Count];
                        for(int i = 0; i<data.Length; i++)
                        {
                            SearchV1 sv1 = new SearchV1();
                            sv1.FromJson(jb.NestedValues[i]);
                            data[i] = sv1;
                        }
                        break;
                    case "object":
                        obj = jb.StringValue;
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