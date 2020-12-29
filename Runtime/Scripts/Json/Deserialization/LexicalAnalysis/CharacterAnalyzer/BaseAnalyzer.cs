namespace OpenAi.Json
{
    /// <summary>
    /// Basic character analyzer used during lexical analysis of json strings
    /// </summary>
    public class BaseAnalyzer : ICharacterAnalyzer
    {
        private StringAnalyzer stringAnalyzer = new StringAnalyzer();

        /// <summary>
        /// Categorizes characters in a json string, assuming they are now between "", and are
        /// not a json string literal
        /// </summary>
        /// <param name="c">Character to analyze</param>
        /// <param name="engage">returns <see cref="StringAnalyzer"/> if character is a "</param>
        /// <returns>Analysis of character</returns>
        public ECharacterAnalyzerResponse Analyze(char c, out ICharacterAnalyzer engage)
        {
            engage = null;

            if (char.IsWhiteSpace(c)) return ECharacterAnalyzerResponse.ExcludeCharacter;

            if( c == '{' || c == '}' || c == '[' || c == ']' || c == ',')
            {
                return ECharacterAnalyzerResponse.Token;
            }

            if (c == '"')
            {
                engage = stringAnalyzer;
                return ECharacterAnalyzerResponse.EngageCharacter;
            }

            return ECharacterAnalyzerResponse.IncludeCharacter;
        }
    }
}
