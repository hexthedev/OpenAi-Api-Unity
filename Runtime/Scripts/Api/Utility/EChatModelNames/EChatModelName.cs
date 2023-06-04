using System;

namespace OpenAi.Api
{
    public static class UTEChatModelName
    {
        public static string GetModelName(EEngineName name)
        {
            switch (name)
            {
                case EEngineName.gpt_35_turbo:
                    return UTModelNames.gpt_35_turbo;
                case EEngineName.BETA_gpt_4:
                    return UTModelNames.BETA_gpt_4;
                case EEngineName.BETA_gpt_4_32k:
                    return UTModelNames.BETA_gpt_4_32k;
            }

            throw new ArgumentException($"Invalid enum value provided when getting chat model name. Value provided: {name}");
        }
    }
}