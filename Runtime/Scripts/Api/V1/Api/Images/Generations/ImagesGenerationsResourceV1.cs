using System;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// Images generation <see href="https://platform.openai.com/docs/api-reference/images/create"/>
    /// </summary>
    public class ImagesGenerationsResourceV1 : AApiResource<ImagesResourceV1>
    {
        /// <inheritdoc/>
        public override string Endpoint => "/generations";

        /// <summary>
        /// Construct with parent
        /// </summary>
        /// <param name="parent"></param>
        public ImagesGenerationsResourceV1(ImagesResourceV1 parent) : base(parent) { }

        /// <summary>
        /// This is the main endpoint of the API. Returns the predicted completion for the given prompt, and can also return the probabilities of alternative tokens at each position if requested. <see href="https://beta.openai.com/docs/api-reference/create-completion"/>. Ignores with <c>request.stream</c> parameter and automatically set to <c>false</c>. To stream, use <see cref="CreateCompletionAsync_EventStream(CompletionRequestV1, Action{ApiResult{CompletionV1}}, Action{int, CompletionV1}, Action)"/> instead
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Asynchronously returns the completion result.  Look in its <see cref="CompletionResult.Choices"/> property for the completions.</returns>
        public async Task<ApiResult<ImagesGenerationsV1>> CreateImagesGenerationAsync(ImagesGenerationsRequestV1 request)
        {
            return await PostAsync<ImagesGenerationsRequestV1, ImagesGenerationsV1>(request);
        }
        /// <summary>
        /// This is the main endpoint of the API. Returns the predicted completion for the given prompt, and can also return the probabilities of alternative tokens at each position if requested. <see href="https://beta.openai.com/docs/api-reference/create-completion"/>. Ignores with <c>request.stream</c> parameter and automatically set to <c>false</c>. To stream, use <see cref="CreateCompletionCoroutine_EventStream(MonoBehaviour, CompletionRequestV1, Action{ApiResult{CompletionV1}}, Action{int, CompletionV1}, Action)"/> instead
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Asynchronously returns the completion result.  Look in its <see cref="CompletionResult.Choices"/> property for the completions.</returns>
        public Coroutine CreateImagesGenerationCoroutine(MonoBehaviour mono, ImagesGenerationsRequestV1 request, Action<ApiResult<ImagesGenerationsV1>> onResult)
        {
            return PostCoroutine(mono, request, onResult);
        }
    }
}