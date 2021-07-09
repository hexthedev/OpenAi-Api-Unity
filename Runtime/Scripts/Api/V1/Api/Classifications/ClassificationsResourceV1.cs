using System;
using System.Threading.Tasks;

using UnityEngine;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// Resource providing classificaiton functionality. Given a query and a set of labeled examples, the model will predict the most likely label for the query. Useful as a drop-in replacement for any ML classification or text-to-label task. <see href="https://beta.openai.com/docs/guides/classifications"/>
    /// </summary>
    public class ClassificationsResourceV1 : AApiResource<OpenAiApiV1>
    {
        /// <inheritdoc/>
        public override string Endpoint => "/classifications";

        /// <summary>
        /// Construct with parent
        /// </summary>
        /// <param name="parent"></param>
        public ClassificationsResourceV1(OpenAiApiV1 parent) : base(parent) { }

        /// <summary>
        /// This is the main endpoint of the API. Classifies the specified query using provided examples. The endpoint first searches over the labeled examples to select the ones most relevant for the particular query. Then, the relevant examples are combined with the query to construct a prompt to produce the final label via the completions endpoint. Labeled examples can be provided via an uploaded file, or explicitly listed in the request using the examples parameter for quick tests and small scale use cases. <see href="https://beta.openai.com/docs/api-reference/classifications/create"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Asynchronously returns classification result</returns>
        public async Task<ApiResult<ClassificationV1>> CreateClassificationAsync(ClassificationRequestV1 request)
        {
            return await PostAsync<ClassificationRequestV1, ClassificationV1>(request);
        }

        /// <summary>
        /// This is the main endpoint of the API. Classifies the specified query using provided examples. The endpoint first searches over the labeled examples to select the ones most relevant for the particular query. Then, the relevant examples are combined with the query to construct a prompt to produce the final label via the completions endpoint. Labeled examples can be provided via an uploaded file, or explicitly listed in the request using the examples parameter for quick tests and small scale use cases. <see href="https://beta.openai.com/docs/api-reference/classifications/create"/>
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Asynchronously returns classification result</returns>
        public Coroutine CreateClassificationCoroutine(MonoBehaviour mono, ClassificationRequestV1 request, Action<ApiResult<ClassificationV1>> onResult)
        {
            return PostCoroutine(mono, request, onResult);
        }
    }
}