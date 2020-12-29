using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAi.Json
{
    public class StringAnalyzer : ICharacterAnalyzer
    {
        public ECharacterAnalyzerResponse Analyze(char c, out ICharacterAnalyzer engage)
        {
            engage = null;
            if (c == '"') return ECharacterAnalyzerResponse.RELEASE_CHARACTER;

            if (c == '\\') return ECharacterAnalyzerResponse.INCLUDE_ESCAPE_CHARACTER;

            return ECharacterAnalyzerResponse.INCLUDE_CHARACTER;
        }
    }
}
