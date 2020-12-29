using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenAi.Json
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