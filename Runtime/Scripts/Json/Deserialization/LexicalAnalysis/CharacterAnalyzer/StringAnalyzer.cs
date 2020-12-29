namespace OpenAi.Json
{
    /// <summary>
    /// Analyzes characters that are between " " in a json string. 
    /// </summary>
    public class StringAnalyzer : ICharacterAnalyzer
    {
        /// <summary>
        /// Analyzes characters that are between " " in a json string. 
        /// </summary>
        /// <param name="c">Character is analyze</param>
        /// <param name="engage">Never used</param>
        /// <returns>Analysis of character</returns>
        public ECharacterAnalyzerResponse Analyze(char c, out ICharacterAnalyzer engage)
        {
            engage = null;
            if (c == '"') return ECharacterAnalyzerResponse.ReleaseCharacter;

            if (c == '\\') return ECharacterAnalyzerResponse.IncludeEscapeCharacter;

            return ECharacterAnalyzerResponse.IncludeCharacter;
        }
    }
}
