using System;
using System.Threading.Tasks;

using UnityEngine;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// Resource related to a specific engine. <see href="https://beta.openai.com/docs/api-reference/retrieve-engine"/>
    /// </summary>
    public class FileResourceV1 : AApiResource<FilesResourceV1>
    {
        private string _endpoint;

        /// <inheritdoc />
        public override string Endpoint => _endpoint;

        /// <summary>
        /// Construct an engine resource with parent and engineId
        /// </summary>
        /// <param name="fileId">The ID of the engine to use for this request</param>
        public FileResourceV1(FilesResourceV1 parent, string fileId) : base(parent)
        {
            _endpoint = $"/{fileId}";
        }

        /// <summary>
        /// Retrieve a file from the OpenAi Api backend <see href="https://beta.openai.com/docs/api-reference/files/retrieve"/>
        /// </summary>
        public async Task<ApiResult<FileV1>> RetrieveEngineAsync() => await GetAsync<FileV1>();

        /// <summary>
        /// Retrieve a file from the OpenAi Api backend <see href="https://beta.openai.com/docs/api-reference/files/retrieve"/>
        /// </summary>
        public Coroutine RetrieveEngineCoroutine(MonoBehaviour mono, Action<ApiResult<FileV1>> onResult) => GetCoroutine(mono, onResult);

        /// <summary>
        /// Delete a file from the OpenAi Api backend <see href="https://beta.openai.com/docs/api-reference/files/delete"/>
        /// </summary>
        public async Task<ApiResult> DeleteFileAsync() => await DeleteAsync();

        /// <summary>
        /// Delete a file from the OpenAi Api backend <see href="https://beta.openai.com/docs/api-reference/files/delete"/>
        /// </summary>
        public Coroutine DeleteFileCoroutine(MonoBehaviour mono, Action<ApiResult<FileV1>> onResult) => GetCoroutine(mono, onResult);
    }
}