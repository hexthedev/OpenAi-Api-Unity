using System;

namespace OpenAi.Api
{
    public enum EChatModelName
    {
        gpt_3_5_turbo,
        gpt_3_5_turbo_0301
    }

    public static class UTEChatModelName
    {
        public static string GetModelName(EChatModelName name)
        {
            switch (name)
            {
                case EChatModelName.gpt_3_5_turbo:
                    return UTChatModelNames.gpt_3_5_turbo;
                case EChatModelName.gpt_3_5_turbo_0301:
                    return UTChatModelNames.gpt_3_5_turbo_0301;
            }

            throw new ArgumentException($"Invalid enum value provided when getting chat model name. Value provided: {name}");
        }
    }
}