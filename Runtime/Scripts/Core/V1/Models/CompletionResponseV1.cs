using System;
using System.Text;

namespace OpenAiApi
{
    public class CompletionResponseV1
    {
        public string id;
        public string obj;
        public int created;
        public string model;
        public CompletionChoiceV1[] choices;
        

        public string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            jb.Add(nameof(id), id);
            jb.Add("object", obj);
            jb.Add(nameof(created), created);
            jb.Add(nameof(model), model);
            jb.AddList(nameof(choices), choices, (c) => c.ToJson());
            jb.EndObject();

            return jb.ToString();
        }

        public void FromJson(JsonObject jsonObj)
        {
            if (jsonObj.Type != EJsonType.Object) throw new Exception("Must be an object");

            foreach(JsonObject jo in jsonObj.NestedValues)
            {
                switch (jo.Name)
                {
                    case nameof(id):
                        id = jo.StringValue;
                        break;
                    case "object":
                        obj = jo.StringValue;
                        break;
                    case nameof(created):
                        created = int.Parse(jo.StringValue);
                        break;
                    case nameof(model):
                        model = jo.StringValue;
                        break;
                    case nameof(choices):
                        CompletionChoiceV1[] choiceArray = new CompletionChoiceV1[jo.NestedValues.Count];
                        for(int i = 0; i<choiceArray.Length; i++)
                        {
                            CompletionChoiceV1 n = new CompletionChoiceV1();
                            n.FromJson(jo.NestedValues[i]);
                            choiceArray[i] = n;
                        }
                        break;

                }
            }
        }
    }
}
