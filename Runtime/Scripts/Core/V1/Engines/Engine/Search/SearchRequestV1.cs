
namespace OpenAiApi
{
    public class SearchRequestV1 : AModelV1
    {
        public string[] documents;
        public string query;

        public override void FromJson(JsonObject json)
        {
            foreach(JsonObject jo in json.NestedValues)
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

        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();
            jb.StartObject();
            jb.AddList(nameof(documents), documents);
            jb.Add(nameof(query), query);
            jb.EndObject();

            return jb.ToString();
        }
    }
}