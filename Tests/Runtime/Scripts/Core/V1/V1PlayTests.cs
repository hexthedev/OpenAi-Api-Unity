using NUnit.Framework;

using OpenAi.Api.V1;

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.TestTools;

namespace OpenAi.Api.Test
{
    public class V1PlayTests
    {

        #region Engines Requests
        [UnityTest]
        public IEnumerator EnginesListCoroutine()
        {
            TestManager test = TestManager.Instance;
            OpenAiApiV1 api = test.CleanAndProvideApi();

            ApiResult<EnginesListV1> result = null;
            yield return api.Engines.ListEnginesCoroutine(test, (r) => result = r);

            Assert.IsNotNull(result);
            Assert.That(result.IsSuccess);
            
            Assert.IsNotNull(result.Result);
            Assert.IsNotEmpty(result.Result.data);
            
            bool containsAda = false;
            foreach(EngineV1 engine in result.Result.data)
            {
                if(engine.id == "ada")
                {
                    containsAda = true;
                    break;
                }
            }

            Assert.That(containsAda);
        }

        [UnityTest]
        public IEnumerator EnginesListAsync()
        {
            TestManager test = TestManager.Instance;
            OpenAiApiV1 api = test.CleanAndProvideApi();

            Task<ApiResult<EnginesListV1>> resTask = api.Engines.ListEnginesAsync();

            while (!resTask.IsCompleted) yield return new WaitForEndOfFrame();

            ApiResult<EnginesListV1> res = resTask.Result;

            Assert.IsNotNull(res);
            Assert.That(res.IsSuccess);

            Assert.IsNotNull(res.Result);
            Assert.IsNotEmpty(res.Result.data);
        }
        #endregion

        #region Engine Requests
        [UnityTest]
        public IEnumerator EngineRetrieveCoroutine()
        {
            TestManager test = TestManager.Instance;
            OpenAiApiV1 api = test.CleanAndProvideApi();

            ApiResult<EngineV1> result = null;
            yield return api.Engines.Engine("ada").RetrieveEngineCoroutine(test, (r) => result = r);

            Assert.IsNotNull(result);
            Assert.That(result.IsSuccess);

            Assert.IsNotNull(result.Result);
            Assert.That(result.Result.id == "ada");
        }

        [UnityTest]
        public IEnumerator EngineRetrieveAsync()
        {
            TestManager test = TestManager.Instance;
            OpenAiApiV1 api = test.CleanAndProvideApi();

            Task<ApiResult<EngineV1>> resultTask = api.Engines.Engine("ada").RetrieveEngineAsync();

            while (!resultTask.IsCompleted) yield return new WaitForEndOfFrame();

            ApiResult<EngineV1> result = resultTask.Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);

            Assert.IsNotNull(result.Result);
            Assert.That(result.Result.id == "ada");
        }
        #endregion

        #region Completion Requests
        [UnityTest]
        public IEnumerator Completions_TestAllRequestParamsString()
        {
            TestManager test = TestManager.Instance;
            OpenAiApiV1 api = test.CleanAndProvideApi();

            ApiResult<CompletionV1> result = null;
            
            CompletionRequestV1 req = new CompletionRequestV1() { 
                prompt = "hello", 
                best_of = 1,
                echo = false,
                frequency_penalty = 0,
                presence_penalty = 0,
                logit_bias = new Dictionary<string, int>() { { "123", -100 }, { "111", 100 } },
                stop = "###",
                logprobs = 0,
                stream = false, 
                max_tokens = 8,
                n = 1,
                temperature = 0,
                top_p = 1
            };

            yield return api.Engines.Engine("ada").Completions.CreateCompletionCoroutine(test, req, (r) => result = r);

            Assert.IsNotNull(result);
            Assert.That(result.IsSuccess);

            Assert.IsNotNull(result.Result);
            Assert.IsNotEmpty(result.Result.choices);
            Assert.That(result.Result.choices.Length > 0);
        }

        [UnityTest]
        public IEnumerator Completions_TestAllRequestParamsArray()
        {
            TestManager test = TestManager.Instance;
            OpenAiApiV1 api = test.CleanAndProvideApi();

            ApiResult<CompletionV1> result = null;

            CompletionRequestV1 req = new CompletionRequestV1()
            {
                prompt = new string[] { "prompt1", "prompt2" },
                best_of = 1,
                echo = false,
                frequency_penalty = 0,
                presence_penalty = 0,
                logit_bias = new Dictionary<string, int>() { { "123", -100 }, { "111", 100 } },
                stop = new string[] { "stop1", "stop2" },
                logprobs = 0,
                stream = false,
                max_tokens = 8,
                n = 1,
                temperature = 0,
                top_p = 1
            };

            yield return api.Engines.Engine("ada").Completions.CreateCompletionCoroutine(test, req, (r) => result = r);

            Assert.IsNotNull(result);
            Assert.That(result.IsSuccess);

            Assert.IsNotNull(result.Result);
            Assert.IsNotEmpty(result.Result.choices);
            Assert.That(result.Result.choices.Length > 0);
        }

        [UnityTest]
        public IEnumerator CompletionsCreateCoroutine()
        {
            TestManager test = TestManager.Instance;
            OpenAiApiV1 api = test.CleanAndProvideApi();

            ApiResult<CompletionV1> result = null;
            CompletionRequestV1 req = new CompletionRequestV1() { prompt = "hello", n = 8 };
            yield return api.Engines.Engine("ada").Completions.CreateCompletionCoroutine(test, req, (r) => result = r);

            Assert.IsNotNull(result);
            Assert.That(result.IsSuccess);

            Assert.IsNotNull(result.Result);
            Assert.IsNotEmpty(result.Result.choices);
            Assert.That(result.Result.choices.Length > 0);
        }

        [UnityTest]
        public IEnumerator CompletionsCreateAsync()
        {
            TestManager test = TestManager.Instance;
            OpenAiApiV1 api = test.CleanAndProvideApi();

            Task<ApiResult<CompletionV1>> resTask = api.Engines.Engine("ada").Completions.CreateCompletionAsync(
                new CompletionRequestV1() { prompt = "hello", max_tokens = 8 }
            );

            while (!resTask.IsCompleted) yield return new WaitForEndOfFrame();

            ApiResult<CompletionV1> res = resTask.Result;

            Assert.IsNotNull(res);
            Assert.That(res.IsSuccess);
            Assert.IsNotNull(res.Result);
        }

        [UnityTest]
        public IEnumerator CompletionsCreateCoroutine_EventStream()
        {
            TestManager test = TestManager.Instance;
            OpenAiApiV1 api = test.CleanAndProvideApi();

            ApiResult<CompletionV1> result = null;
            List<CompletionV1> partials = new List<CompletionV1>();
            bool isComplete = false;

            CompletionRequestV1 req = new CompletionRequestV1() { prompt = "hello", n = 8 };
            yield return api.Engines.Engine("ada").Completions.CreateCompletionCoroutine_EventStream(
                test, 
                req, 
                (r) => result = r,
                (i, l) => partials.Add(l),
                () => isComplete = true
            );

            float timer = 10f;
            while (!isComplete)
            {
                timer -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            Assert.That(isComplete);

            Assert.IsNotNull(result);
            Assert.That(result.IsSuccess);
            Assert.IsNotEmpty(partials);
        }

        [UnityTest]
        public IEnumerator CompletionsCreateAsync_EventStream()
        {
            TestManager test = TestManager.Instance;
            OpenAiApiV1 api = test.CleanAndProvideApi();

            ApiResult<CompletionV1> result = null;
            List<CompletionV1> completions = new List<CompletionV1>();
            bool isComplete = false;

            Task engineTask = api.Engines.Engine("davinci").Completions.CreateCompletionAsync_EventStream(
                new CompletionRequestV1() { prompt = "hello", max_tokens = 8, stream = true },
                (r) => result = r,
                (i, c) => completions.Add(c),
                () => isComplete = true
            );

            while (!engineTask.IsCompleted) yield return new WaitForEndOfFrame();

            Assert.IsNotNull(result);
            Assert.That(result.IsSuccess);

            Assert.That(isComplete);

            Assert.That(completions.Count > 0);
        }
        #endregion

        #region Search Requests
        [UnityTest]
        public IEnumerator SearchSearchCoroutine()
        {
            TestManager test = TestManager.Instance;
            OpenAiApiV1 api = test.CleanAndProvideApi();

            ApiResult<SearchListV1> result = null;
            SearchRequestV1 req = new SearchRequestV1() { documents = new string[] { "doc1", "doc2" }, query = "is this a doc"};
            yield return api.Engines.Engine("ada").Search.SearchCoroutine(test, req, (r) => result = r);

            Assert.IsNotNull(result);
            Assert.That(result.IsSuccess);
            Assert.IsNotNull(result.Result);
            Assert.IsNotEmpty(result.Result.data);
            Assert.That(result.Result.data.Length == 2);
        }

        [UnityTest]
        public IEnumerator SearchSearchAsync()
        {
            TestManager test = TestManager.Instance;
            OpenAiApiV1 api = test.CleanAndProvideApi();

            Task<ApiResult<SearchListV1>> resTask = api.Engines.Engine("davinci").Search.SearchAsync(
                new SearchRequestV1() { documents = new string[] { "Hey baby", "I am a robot" }, query = "query?" }
            );

            while (!resTask.IsCompleted) yield return new WaitForEndOfFrame();

            ApiResult<SearchListV1> res = resTask.Result;

            Assert.IsNotNull(res);
            Assert.That(res.IsSuccess);

            Assert.IsNotNull(res.Result);

            Assert.IsNotEmpty(res.Result.data);
            Assert.That(res.Result.data.Length == 2);
        }
        #endregion
    }
}