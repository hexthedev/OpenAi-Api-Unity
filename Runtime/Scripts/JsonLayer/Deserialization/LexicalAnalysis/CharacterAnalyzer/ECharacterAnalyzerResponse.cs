using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAi.Json
{
    /// <summary>
    /// Possible character analysis results that can occur during deserailization
    /// </summary>
    public enum ECharacterAnalyzerResponse
    {
        /// <summary>
        /// This character alone is a token and should be added directly
        /// </summary>
        Token,

        /// <summary>
        /// This character should be included in a token that is being generated
        /// </summary>
        IncludeCharacter,

        /// <summary>
        /// Using \ as anchor, include escape character by skipping next character
        /// </summary>
        IncludeEscapeCharacter,

        /// <summary>
        /// This character should be excluded from token generation
        /// </summary>
        ExcludeCharacter,

        /// <summary>
        /// This character requires the engament of another analyzer, provided in out ICharacterAnalyzer
        /// </summary>
        EngageCharacter,

        /// <summary>
        /// This characher requires the release of the current analyzer and reversion to the last used analyzer
        /// </summary>
        ReleaseCharacter
    }
}
