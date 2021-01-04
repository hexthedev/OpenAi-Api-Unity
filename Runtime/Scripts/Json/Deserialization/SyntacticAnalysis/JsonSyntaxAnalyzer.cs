using System;
using System.Collections.Generic;

namespace OpenAi.Json
{
    /// <summary>
    /// Parses arrays of json tokens and outputs <see cref="JsonObject"/>
    /// </summary>
    public static class JsonSyntaxAnalyzer
    {
        /// <summary>
        /// Parse an array of json tokens
        /// </summary>
        /// <param name="syntax">array of tokens</param>
        /// <returns><see cref="JsonObject"/> representation of deserialized object</returns>
        public static JsonObject Parse(string[] syntax)
        {
            if (syntax == null || syntax.Length < 2) throw new OpenAiJsonException("Failed to parse syntax. Either null, or length < 2");

            JsonObject obj = new JsonObject();

            switch (syntax[0])
            {
                case "{":
                    obj.Type = EJsonType.Object;
                    ParseObject(obj, syntax, 1);
                    return obj;
                case "[":
                    obj.Type = EJsonType.List;
                    ParseList(obj, syntax, 1);
                    return obj;
            }

            throw new OpenAiJsonException("Failed to parse. Unknown error");
        }

        private static int ParseObject(JsonObject parent, string[] syntax, int index)
        {
            int i = index;
            for (; i<syntax.Length; i++)
            {
                i = ParseValue(parent, syntax, i);
                if (syntax[i] == "}") return i + 1;
            }
            throw new OpenAiJsonException($"Failed to parse object at token { syntax[i] }");
        }

        private static int ParseList(JsonObject parent, string[] syntax, int index)
        {
            int i = index;
            for (; i < syntax.Length; i++)
            {
                i = ParseListValue(parent, syntax, i);
                if (syntax[i] == "]") return i + 1;
            }
            throw new OpenAiJsonException($"Failed to parse list at token { syntax[i] }");
        }

        private static int ParseValue(JsonObject parent, string[] syntax, int index)
        {
            // Validate
            if (syntax[index + 1] != ":") throw new OpenAiJsonException($"Failed to value at token { syntax[index] } because it is not preceeded by a :, prceeded by { syntax[index+1] }");

            JsonObject val = new JsonObject();
            val.Name = syntax[index];

            if (parent.NestedValues == null) parent.NestedValues = new List<JsonObject>();
            parent.NestedValues.Add(val);

            switch (syntax[index + 2]) 
            {
                case "{":
                    val.Type = EJsonType.Object;
                    return ParseObject(val, syntax, index + 3);
                case "[":
                    val.Type = EJsonType.List;
                    return ParseList(val, syntax, index + 3);
            }

            val.Type = EJsonType.Value;
            val.StringValue = syntax[index + 2];

            return index + 3;
        }

        public static int ParseListValue(JsonObject parent, string[] syntax, int index)
        {
            // Validate
            JsonObject val = new JsonObject();

            if (parent.NestedValues == null) parent.NestedValues = new List<JsonObject>();
            parent.NestedValues.Add(val);

            switch (syntax[index])
            {
                case "{":
                    val.Type = EJsonType.Object;
                    return ParseObject(val, syntax, index + 1);
                case "[":
                    val.Type = EJsonType.List;
                    return ParseList(val, syntax, index + 1);
            }

            val.Type = EJsonType.Value;
            val.StringValue = syntax[index];

            return index + 1;
        }
    }
}
