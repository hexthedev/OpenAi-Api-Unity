namespace OpenAi.Json
{
    /// <summary>
    /// Capable of analyzing a single character for the purpose of deserialization
    /// </summary>
    public interface ICharacterAnalyzer
    {
        /// <summary>
        /// Analyzes a character and returns result based on the type of character it is.
        /// </summary>
        /// <param name="c">The character to analyze</param>
        /// <param name="engage">The new character analyzer to engage, if the character represents the start of a unique section</param>
        /// <returns></returns>
        ECharacterAnalyzerResponse Analyze(char c, out ICharacterAnalyzer engage);
    }
}
