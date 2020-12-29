using System.Collections.Generic;

namespace OpenAi.Json
{
    public class JsonObject
    {
        public EJsonType Type;
        public string Name;
        public string StringValue;
        public List<JsonObject> NestedValues;
    }
}
