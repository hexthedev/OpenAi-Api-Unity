namespace OpenAi.Api.V1
{
    /// <summary>
    /// Images <see href="https://platform.openai.com/docs/api-reference/images"/>
    /// </summary>
    public class ImagesResourceV1 : AApiResource<OpenAiApiV1>
    {
        /// <inheritdoc/>
        public override string Endpoint => "/images";

        /// <summary>
        /// Construct with parent
        /// </summary>
        /// <param name="parent"></param>
        public ImagesResourceV1(OpenAiApiV1 parent) : base(parent) { Generations = new ImagesGenerationsResourceV1(this); }

        /// <summary>
        /// Images creation. <see href="https://platform.openai.com/docs/api-reference/images/create"/>
        /// </summary>
        public ImagesGenerationsResourceV1 Generations { get; private set; }
    }
}