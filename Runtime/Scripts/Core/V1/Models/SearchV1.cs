using UnityEngine;

namespace OpenAiApi
{
    public class SearchV1 : AModelV1
    {
        public int document;
        public string obj;
        public float score;

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