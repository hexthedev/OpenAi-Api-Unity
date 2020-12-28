using NUnit.Framework;
using UnityEngine;

namespace OpenAiApi
{
    /// <summary>
    /// All tests will only work if you have a secret key as the environment variable OPENAI_PRIVATE_KEY
    /// </summary>
    public class OpenAiApiV1Tests
    {


        [Test]
        public async void OpenAiApiV1TestCompletionsCreate()
        {
            string key = UTTestAuth.GetAndValidateAuthKey();
            OpenAiApiV1 api = new OpenAiApiV1(key);

            ApiResult<CompletionV1> res =  await api.Engines.Engine("davinci").Completions.CreateAsync(
                new CompletionRequestV1() { prompt = "hello", max_tokens = 8 }
            );

            Assert.IsNotNull(res.IsSuccess);
        }

        [Test]
        public async void OpenAiApiV1TestCompletionsCreateStream()
        {
            string key = UTTestAuth.GetAndValidateAuthKey();
            OpenAiApiV1 api = new OpenAiApiV1(key);

            await api.Engines.Engine("davinci").Completions.CreateAsync_EventStream(
                new CompletionRequestV1() { prompt = "hello", max_tokens = 8, stream = true },
                (r) => Debug.Log(r),
                (i, c) => Debug.Log($"This actaully worked {c.ToJson()}")
            ); ;
        }

        [Test]
        public async void OpenAiApiV1TestCompletionsSearch()
        {
            string key = UTTestAuth.GetAndValidateAuthKey();
            OpenAiApiV1 api = new OpenAiApiV1(key);

            ApiResult<SearchListV1> res = await api.Engines.Engine("davinci").Search.SearchAsync(
                new SearchRequestV1() { documents = new string[] { "Hey baby", "I am a robot" }, query = "query?" }
            );

            Assert.IsNotNull(res.IsSuccess);
        }

        /// <summary>
        /// This will only work if you have a secret key as the environment variable OPENAI_PRIVATE_KEY
        /// </summary>
        [Test]
        public async void OpenAiApiV1TestEnginesList()
        {
            string key = UTTestAuth.GetAndValidateAuthKey();

            OpenAiApiV1 api = new OpenAiApiV1(key);
            ApiResult<EnginesListV1> res = await api.Engines.ListAsync();
            
            Assert.IsNotNull(res);
        }

        /// <summary>
        /// This will only work if you have a secret key as the environment variable OPENAI_PRIVATE_KEY
        /// </summary>
        [Test]
        public async void OpenAiApiV1TestEngineRetrieve()
        {
            string key = UTTestAuth.GetAndValidateAuthKey();

            OpenAiApiV1 api = new OpenAiApiV1(key);
            ApiResult<EngineV1> res = await api.Engines.Engine("ada").RetrieveAsync();

            Assert.IsNotNull(res);
        }

        
    }
}