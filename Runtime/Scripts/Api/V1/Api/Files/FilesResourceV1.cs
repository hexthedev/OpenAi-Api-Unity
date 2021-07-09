using System;
using System.Threading.Tasks;

using UnityEngine;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// Provides functions to list and upload files
    /// </summary>
    public class FilesResourceV1 : AApiResource<OpenAiApiV1>
    {
        /// <inheritdoc />
        public override string Endpoint => "/files";

        /// <summary>
        /// Construct Files resource for listing and manging files
        /// </summary>
        /// <param name="parent"></param>
        public FilesResourceV1(OpenAiApiV1 parent) : base(parent) { }

        /// <summary>
        /// Construct an engine resource using an engine id. The engine id must be a valid engine.
        /// <see href="https://beta.openai.com/docs/api-reference/retrieve-engine"/>
        /// </summary>
        /// <param name="engineId">The ID of the engine to use for this request</param>
        //public FilesResourceV1 Engine(string engineId) => new FilesResourceV1(this, );

        /// <summary>
        /// Lists the currently available files <see href="https://beta.openai.com/docs/api-reference/files/list"/>
        /// </summary>
        public async Task<ApiResult<FilesListV1>> ListFilesAsync() => await GetAsync<FilesListV1>();

        /// <summary>
        /// Lists the currently available files <see href="https://beta.openai.com/docs/api-reference/files/list"/>
        /// </summary>
        public Coroutine ListFilesCoroutine(MonoBehaviour mono, Action<ApiResult<FilesListV1>> onResult) => GetCoroutine(mono, onResult);
    }
}