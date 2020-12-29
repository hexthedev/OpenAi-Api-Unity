using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAi.Json
{
    public class BaseAnalyzer : ICharacterAnalyzer
    {
        public StringAnalyzer stringAnalyzer = new StringAnalyzer();

        public ECharacterAnalyzerResponse Analyze(char c, out ICharacterAnalyzer engage)
        {
            engage = null;

            if (char.IsWhiteSpace(c)) return ECharacterAnalyzerResponse.EXCLUDE_CHARACTER;

            if( c == '{' || c == '}' || c == '[' || c == ']' || c == ',')
            {
                return ECharacterAnalyzerResponse.TOKEN;
            }

            if (c == '"')
            {
                engage = stringAnalyzer;
                return ECharacterAnalyzerResponse.ENGAGE_CHARACTER;
            }

            return ECharacterAnalyzerResponse.INCLUDE_CHARACTER;
        }
    }
}
