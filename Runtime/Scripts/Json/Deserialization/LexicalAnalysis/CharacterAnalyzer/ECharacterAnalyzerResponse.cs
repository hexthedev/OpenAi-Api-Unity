using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAi.Json
{
    public enum ECharacterAnalyzerResponse
    {
        /// <summary>
        /// This character alone is a token and should be added directly
        /// </summary>
        TOKEN,

        /// <summary>
        /// This character should be included in a token that is being generated
        /// </summary>
        INCLUDE_CHARACTER,

        /// <summary>
        /// Using \ as anchor, include escape character by skipping next character
        /// </summary>
        INCLUDE_ESCAPE_CHARACTER,

        /// <summary>
        /// This character should be excluded from token generation
        /// </summary>
        EXCLUDE_CHARACTER,

        /// <summary>
        /// This character requires the engament of another analyzer, provided in out ICharacterAnalyzer
        /// </summary>
        ENGAGE_CHARACTER,

        /// <summary>
        /// This characher requires the release of the current analyzer and reversion to the last used analyzer
        /// </summary>
        RELEASE_CHARACTER
    }
}
