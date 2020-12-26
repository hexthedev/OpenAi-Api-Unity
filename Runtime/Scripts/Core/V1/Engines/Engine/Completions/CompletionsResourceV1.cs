using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace OpenAiApi
{

    /// <summary>
    /// Text generation is the core function of the API. You give the API a prompt, and it generates a completion. The way you “program” the API to do a task is by simply describing the task in plain english or providing a few written examples. This simple approach works for a wide range of use cases, including summarization, translation, grammar correction, question answering, chatbots, composing emails, and much more (see the prompt library for inspiration).
    /// </summary>
    public class CompletionsResourceV1 : AResource<EngineResource>
    {
        public override string Endpoint => "/completions";

        public CompletionsResourceV1(EngineResource parent) : base(parent) { }

        /// <summary>
        /// Ask the API to complete the prompt(s) using the specified request.  This is non-streaming, so it will wait until the API returns the full result.
        /// </summary>
        /// <param name="request">The request to send to the API.  This does not fall back to default values specified in <see cref="DefaultCompletionRequestArgs"/>.</param>
        /// <returns>Asynchronously returns the completion result.  Look in its <see cref="CompletionResult.Choices"/> property for the completions.</returns>
        public async Task<CompletionV1> Create(CompletionRequestV1 request)
        {
            request.stream = false;
            return await PostAsync<CompletionRequestV1, CompletionV1>(request);
        }

        #region Streaming

        /// <summary>
        /// Ask the API to complete the prompt(s) using the specified request, and stream the results to the <paramref name="resultHandler"/> as they come in.
        /// If you are on the latest C# supporting async enumerables, you may prefer the cleaner syntax of <see cref="StreamCompletionEnumerableAsync(CompletionRequest)"/> instead.
        /// </summary>
        /// <param name="request">The request to send to the API.  This does not fall back to default values specified in <see cref="DefaultCompletionRequestArgs"/>.</param>
        /// <param name="resultHandler">An action to be called as each new result arrives, which includes the index of the result in the overall result set.</param>
        public async Task CreateStream(CompletionRequestV1 request, Action<int, CompletionV1> resultHandler)
        {
            request.stream = true;
            await PostEventStreamAsync(request, resultHandler);
        }
        #endregion
    }
}