using OpenAi.Json;

namespace OpenAi.Api.V1
{
    public class SearchListV1 : AModelV1
    {
        public SearchV1[] data;
        public string obj;

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

        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();
            jb.StartObject();
            jb.AddList(nameof(data), data);
            jb.Add("object", obj);
            jb.EndObject();

            return jb.ToString();
        }
    }
}