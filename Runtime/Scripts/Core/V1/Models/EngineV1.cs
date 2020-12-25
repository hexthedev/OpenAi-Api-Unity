namespace OpenAiApi
{
    public class EngineV1 : AModelV1
    {
        public string id;
        public string obj;
        public string owner;
        public bool? ready;

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
