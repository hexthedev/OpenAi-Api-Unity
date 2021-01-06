using System;

namespace OpenAi.Api
{
    public enum EEngineName
    {
        ada,
        babbage,
        content_filter_alpha_c4,
        content_filter_dev,
        curie,
        cursing_filter_v6,
        davinci,
        instruct_curie_beta,
        instruct_davinci_beta
    }

    public static class UTEEngineName
    {
        public static string GetEngineName(EEngineName name)
        {
            switch (name)
            {
                case EEngineName.ada:
                    return UTEngineNames.ada;
                case EEngineName.babbage:
                    return UTEngineNames.babbage;
                case EEngineName.content_filter_alpha_c4:
                    return UTEngineNames.content_filter_alpha_c4;
                case EEngineName.content_filter_dev:
                    return UTEngineNames.content_filter_dev;
                case EEngineName.curie:
                    return UTEngineNames.curie;
                case EEngineName.cursing_filter_v6:
                    return UTEngineNames.cursing_filter_v6;
                case EEngineName.davinci:
                    return UTEngineNames.davinci;
                case EEngineName.instruct_curie_beta:
                    return UTEngineNames.instruct_curie_beta;
                case EEngineName.instruct_davinci_beta:
                    return UTEngineNames.instruct_davinci_beta;
            }

            throw new ArgumentException($"Invalid enum value provided when getting engine name. Value provided: {name}");
        }
    }
}