namespace OpenAiApi
{
    class EnginesListResponseV1 : AModelV1
    {
        public EngineModelV1[] data;
        public string obj;

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
                        data = ArrayFromJson<EngineModelV1>(jo);
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
