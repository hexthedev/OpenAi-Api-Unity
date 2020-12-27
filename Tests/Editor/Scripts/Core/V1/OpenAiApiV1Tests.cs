using NUnit.Framework;

using System;
using System.IO;
using System.Security.Authentication;
using System.Text;

using UnityEngine;

namespace OpenAiApi
{
    /// <summary>
    /// All tests will only work if you have a secret key as the environment variable OPENAI_PRIVATE_KEY
    /// </summary>
    public class OpenAiApiV1Tests
    {
        private const string cOpenAiKeyName = "OPENAI_PRIVATE_KEY";


        [Test]
        public async void OpenAiApiV1TestCompletionsCreate()
        {
            string key = GetAndValidateAuthKey();
            OpenAiApiV1 api = new OpenAiApiV1(key);

            ApiResult<CompletionV1> res =  await api.Engines.Engine("davinci").Completions.Create(
                new CompletionRequestV1() { prompt = "hello", max_tokens = 8 }
            );

            Assert.IsNotNull(res.IsSuccess);
        }

        [Test]
        public async void OpenAiApiV1TestCompletionsCreateStream()
        {
            string key = GetAndValidateAuthKey();
            OpenAiApiV1 api = new OpenAiApiV1(key);

            await api.Engines.Engine("davinci").Completions.CreateStream(
                new CompletionRequestV1() { prompt = "hello", max_tokens = 8, stream = true },
                (r) => Debug.Log(r),
                (i, c) => Debug.Log($"This actaully worked {c.ToJson()}")
            ); ;
        }

        [Test]
        public async void OpenAiApiV1TestCompletionsSearch()
        {
            string key = GetAndValidateAuthKey();
            OpenAiApiV1 api = new OpenAiApiV1(key);

            ApiResult<SearchListV1> res = await api.Engines.Engine("davinci").Search.Search(
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
            string key = GetAndValidateAuthKey();

            OpenAiApiV1 api = new OpenAiApiV1(key);
            ApiResult<EnginesListV1> res = await api.Engines.List();
            
            Assert.IsNotNull(res);
        }

        /// <summary>
        /// This will only work if you have a secret key as the environment variable OPENAI_PRIVATE_KEY
        /// </summary>
        [Test]
        public async void OpenAiApiV1TestEngineRetrieve()
        {
            string key = GetAndValidateAuthKey();

            OpenAiApiV1 api = new OpenAiApiV1(key);
            ApiResult<EngineV1> res = await api.Engines.Engine("ada").Retrieve();

            Assert.IsNotNull(res);
        }

        private string GetAndValidateAuthKey()
        {
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string authPath = $"{userPath}/.openai/key.txt";
            FileInfo fi = new FileInfo(authPath);

            if (!fi.Exists) throw new AuthenticationException($"No authentication file exists at {authPath}");

            string key = null;
            using (FileStream fs = fi.OpenRead())
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
                key = Encoding.UTF8.GetString(buffer);
            }

            return key;
        }
    }
}