using System.Collections.Generic;
using System.Text;

namespace OpenAi.Json
{
    /// <summary>
    /// Simple single pass lexical analysis of a JSON string
    /// </summary>
    public static class JsonLexer
    {
        /// <summary>
        /// Preforms a lexical analysis of a JSON string and returns an array of tokens.
        /// </summary>
        /// <param name="json">json string to analyze</param>
        /// <returns>array of tokens</returns>
        public static string[] Lex(string json)
        {
            StringBuilder sb = new StringBuilder();
            bool generatingToken = false;

            Stack<ICharacterAnalyzer> analyzer = new Stack<ICharacterAnalyzer>();
            analyzer.Push(new BaseAnalyzer());

            List<string> tokens = new List<string>();

            // Analyzes each character in the string based on the currently set analyzer. 
            // Based on analysis adds tokens to the tokens list.
            for (int i = 0; i<json.Length; i++)
            {
                ECharacterAnalyzerResponse res = analyzer.Peek().Analyze(json[i], out ICharacterAnalyzer engage);

                switch (res)
                {
                    case ECharacterAnalyzerResponse.Token:
                        if (generatingToken) AddToken();
                        tokens.Add($"{json[i]}");
                        break;

                    case ECharacterAnalyzerResponse.IncludeCharacter:
                        if (!generatingToken) generatingToken = true;
                        sb.Append(json[i]);
                        break;

                    case ECharacterAnalyzerResponse.IncludeEscapeCharacter:
                        if (!generatingToken) generatingToken = true;
                        sb.Append(json[i]);
                        sb.Append(json[i++]);
                        break;

                    case ECharacterAnalyzerResponse.ExcludeCharacter:
                        if (generatingToken) AddToken();
                        break;

                    // character to engage or release another analyzer
                    case ECharacterAnalyzerResponse.EngageCharacter:
                        if (generatingToken) AddToken();
                        analyzer.Push(engage);
                        break;

                    case ECharacterAnalyzerResponse.ReleaseCharacter:
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