using System;
using System.Threading.Tasks;

using UnityEngine;

namespace OpenAi.Api.V1
{

    /// <summary>
    /// Resource providing completions functionality. Text generation is the core function of the API. You give the API a prompt, and it generates a completion. The way you “program” the API to do a task is by simply describing the task in plain english or providing a few written examples. This simple approach works for a wide range of use cases, including summarization, translation, grammar correction, question answering, chatbots, composing emails, and much more (see the prompt library for inspiration). <see href="https://beta.openai.com/docs/examples"/>
    /// </summary>
    public class CompletionsResourceV1 : AApiResource<EngineResource>
    {
        /// <inheritdoc/>
        public override string Endpoint => "/completions";

        /// <summary>
        /// Construct with parent
        /// </summary>
        /// <param name="parent"></param>
        public CompletionsResourceV1(EngineResource parent) : base(parent) { }

        /// <summary>
        /// This is the main endpoint of the API. Returns the predicted completion for the given prompt, and can also return the probabilities of alternative tokens at each position if requested. <see href="https://beta.openai.com/docs/api-reference/create-completion"/>. Ignores with <c>request.stream</c> parameter and automatically set to <c>false</c>. To stream, use <see cref="CreateAsync_EventStream(CompletionRequestV1, Action{ApiResult{CompletionV1}}, Action{int, CompletionV1}, Action)"/> instead
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Asynchronously returns the completion result.  Look in its <see cref="CompletionResult.Choices"/> property for the completions.</returns>
        public async Task<ApiResult<CompletionV1>> CreateAsync(CompletionRequestV1 request)
        {
            request.stream = false;
            return await PostAsync<CompletionRequestV1, CompletionV1>(request);
        }
        /// <summary>
        /// This is the main endpoint of the API. Returns the predicted completion for the given prompt, and can also return the probabilities of alternative tokens at each position if requested. <see href="https://beta.openai.com/docs/api-reference/create-completion"/>. Ignores with <c>request.stream</c> parameter and automatically set to <c>false</c>. To stream, use <see cref="CreateCoroutine_EventStream(MonoBehaviour, CompletionRequestV1, Action{ApiResult{CompletionV1}}, Action{int, CompletionV1}, Action)"/> instead
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Asynchronously returns the completion result.  Look in its <see cref="CompletionResult.Choices"/> property for the completions.</returns>
        public Coroutine CreateCoroutine(MonoBehaviour mono, CompletionRequestV1 request, Action<ApiResult<CompletionV1>> onResult)
        {
            request.stream = false;
            return PostCoroutine(mono, request, onResult);
        }

        #region Streaming
        /// <summary>
        /// This is the main endpoint of the API. Returns the predicted completion for the given prompt, and can also return the probabilities of alternative tokens at each position if requested. <see href="https://beta.openai.com/docs/api-reference/create-completion"/>. Ignores with <c>request.stream</c> parameter and automatically set to <c>true</c>. To stream, use <see cref="CreateAsync(CompletionRequestV1)"/> instead
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Asynchronously returns the completion result.  Look in its <see cref="CompletionResult.Choices"/> property for the completions.</returns>
        public async Task CreateAsync_EventStream(CompletionRequestV1 request, Action<ApiResult<CompletionV1>> onRequestStatus, Action<int, CompletionV1> onPartialResult, Action onCompletion = null)
        {
            request.stream = true;
            await PostAsync_EventStream(request, onRequestStatus, onPartialResult, onCompletion);
        }

        /// <summary>
        /// This is the main endpoint of the API. Returns the predicted completion for the given prompt, and can also return the probabilities of alternative tokens at each position if requested. <see href="https://beta.openai.com/docs/api-reference/create-completion"/>. Ignores with <c>request.stream</c> parameter and automatically set to <c>true</c>. To stream, use <see cref="CreateCoroutine(MonoBehaviour, CompletionRequestV1, Action{ApiResult{CompletionV1}})"/> instead
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Asynchronously returns the completion result.  Look in its <see cref="CompletionResult.Choices"/> property for the completions.</returns>
        public Coroutine CreateCoroutine_EventStream(MonoBehaviour mono, CompletionRequestV1 request, Action<ApiResult<CompletionV1>> onRequestStatus, Action<int, CompletionV1> onPartialResult, Action onCompletion = null)
        {
            request.stream = true;
            return PostCoroutine_EventStream(mono, request, onRequestStatus, onPartialResult, onCompletion);
        }
        #endregion
    }
}