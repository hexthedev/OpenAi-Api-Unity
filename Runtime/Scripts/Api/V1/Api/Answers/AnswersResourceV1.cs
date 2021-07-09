using System;
using System.Threading.Tasks;

using UnityEngine;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// Answers the specified question using the provided documents and examples. The endpoint first searches over provided documents or files to find relevant context.The relevant context is combined with the provided examples and question to create the prompt for completion. <see cref="https://beta.openai.com/docs/api-reference/answers/create#answers/create-examples"/>
    /// </summary>
    public class AnswersResourceV1 : AApiResource<OpenAiApiV1>
    {
        /// <inheritdoc/>
        public override string Endpoint => "/answers";

        /// <summary>
        /// Construct with parent
        /// </summary>
        /// <param name="parent"></param>
        public AnswersResourceV1(OpenAiApiV1 parent) : base(parent) { }

        /// <summary>
        /// Answers the specified question using the provided documents and examples. The endpoint first searches over provided documents or files to find relevant context.The relevant context is combined with the provided examples and question to create the prompt for completion. <see cref="https://beta.openai.com/docs/api-reference/answers/create#answers/create-examples"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Asynchronously returns classification result</returns>
        public async Task<ApiResult<AnswerV1>> CreateAnswerAsync(AnswerRequestV1 request)
        {
            return await PostAsync<AnswerRequestV1, AnswerV1>(request);
        }

        /// <summary>
        /// Answers the specified question using the provided documents and examples. The endpoint first searches over provided documents or files to find relevant context.The relevant context is combined with the provided examples and question to create the prompt for completion. <see cref="https://beta.openai.com/docs/api-reference/answers/create#answers/create-examples"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Asynchronously returns classification result</returns>
        public Coroutine CreateAnswerCoroutine(MonoBehaviour mono, AnswerRequestV1 request, Action<ApiResult<AnswerV1>> onResult)
        {
            return PostCoroutine(mono, request, onResult);
        }
    }
}