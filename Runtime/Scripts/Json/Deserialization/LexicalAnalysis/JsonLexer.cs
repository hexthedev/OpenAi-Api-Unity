using System.Collections.Generic;
using System.Text;

using UnityEngine;

namespace OpenAi.Json
{
    /// <summary>
    /// Simple single pass lexical analysis of a JSON object
    /// </summary>
    public static class JsonLexer
    {
        /// <summary>
        /// preforms a lexical analysis of a JSON string and
        /// returns a list of tokens.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string[] Lex(string json)
        {
            StringBuilder sb = new StringBuilder();
            bool generatingToken = false;

            Stack<ICharacterAnalyzer> analyzer = new Stack<ICharacterAnalyzer>();
            analyzer.Push(new BaseAnalyzer());

            List<string> tokens = new List<string>();

            for(int i = 0; i<json.Length; i++)
            {
                ECharacterAnalyzerResponse res = analyzer.Peek().Analyze(json[i], out ICharacterAnalyzer engage);

                switch (res)
                {
                    case ECharacterAnalyzerResponse.TOKEN:
                        if (generatingToken) AddToken();
                        tokens.Add($"{json[i]}");
                        break;

                    case ECharacterAnalyzerResponse.INCLUDE_CHARACTER:
                        if (!generatingToken) generatingToken = true;
                        sb.Append(json[i]);
                        break;

                    case ECharacterAnalyzerResponse.INCLUDE_ESCAPE_CHARACTER:
                        if (!generatingToken) generatingToken = true;
                        sb.Append(json[i]);
                        sb.Append(json[i++]);
                        break;

                    case ECharacterAnalyzerResponse.EXCLUDE_CHARACTER:
                        if (generatingToken) AddToken();
                        break;

                    // character to engage or release another analyzer
                    case ECharacterAnalyzerResponse.ENGAGE_CHARACTER:
                        if (generatingToken) AddToken();
                        analyzer.Push(engage);
                        break;

                    case ECharacterAnalyzerResponse.RELEASE_CHARACTER:
                        AddToken();
                        analyzer.Pop();
                        break;
                }
            }

            return tokens.ToArray();

            void AddToken()
            {
                generatingToken = false;
                tokens.Add(sb.ToString());
                sb.Clear();
            }
        }
    }
}