using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenAiApi
{
    public static class JsonDeserializer
    {
        public static JsonObject FromJson(string json)
        {
            string[] tokens = JsonLexer.Lex(json);
            return JsonSyntaxAnalyzer.Parse(tokens);
        }
    }
}