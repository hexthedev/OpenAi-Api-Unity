namespace OpenAiApi
{
    /// <summary>
    /// Analyzes a character
    /// </summary>
    public interface ICharacterAnalyzer
    {
        public ECharacterAnalyzerResponse Analyze(char c, out ICharacterAnalyzer engage);
    }
}
