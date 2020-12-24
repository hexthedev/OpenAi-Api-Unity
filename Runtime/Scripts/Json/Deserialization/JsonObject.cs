using System.Collections.Generic;

namespace OpenAiApi
{
    public class JsonObject
    {
        public EJsonType Type;
        public string Name;
        public string StringValue;
        public List<JsonObject> NestedValues;
    }
}
