using NUnit.Framework;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.TestTools;

namespace OpenAiApi
{
    public class V1PlayTests
    {
        [UnityTest]
        public IEnumerator EnginesListCoroutine()
        {
            string key = UTTestAuth.GetAndValidateAuthKey();
            OpenAiApiV1 api = new OpenAiApiV1(key);

            EmptyMono test = new GameObject("test", typeof(EmptyMono)).GetComponent<EmptyMono>();

            ApiResult<EnginesListV1> result = null;
            yield return api.Engines.ListCoroutine(test, (r) => result = r);

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
            test.DestroySelf();
        }

        [UnityTest]
        public IEnumerator EngineCoroutine()
        {
            string key = UTTestAuth.GetAndValidateAuthKey();
            OpenAiApiV1 api = new OpenAiApiV1(key);

            EmptyMono test = new GameObject("test", typeof(EmptyMono)).GetComponent<EmptyMono>();

            ApiResult<EngineV1> result = null;
            yield return api.Engines.Engine("ada").RetrieveCoroutine(test, (r) => result = r);

            Assert.IsNotNull(result);
            Assert.That(result.IsSuccess);

            Assert.IsNotNull(result.Result);
            Assert.That(result.Result.id == "ada");
            test.DestroySelf();
        }


        [UnityTest]
        public IEnumerator CompletionsCreateCoroutine()
        {
            string key = UTTestAuth.GetAndValidateAuthKey();
            OpenAiApiV1 api = new OpenAiApiV1(key);

            EmptyMono test = new GameObject("test", typeof(EmptyMono)).GetComponent<EmptyMono>();

            ApiResult<CompletionV1> result = null;
            CompletionRequestV1 req = new CompletionRequestV1() { prompt = "hello", n = 8 };
            yield return api.Engines.Engine("ada").Completions.CreateCoroutine(test, req, (r) => result = r);

            Assert.IsNotNull(result);
            Assert.That(result.IsSuccess);

            Assert.IsNotNull(result.Result);
            Assert.IsNotEmpty(result.Result.choices);
            Assert.That(result.Result.choices.Length > 0);
            test.DestroySelf();
        }

        [UnityTest]
        public IEnumerator CompletionsCreateCoroutine_EventStream()
        {
            string key = UTTestAuth.GetAndValidateAuthKey();
            OpenAiApiV1 api = new OpenAiApiV1(key);

            EmptyMono test = new GameObject("test", typeof(EmptyMono)).GetComponent<EmptyMono>();

            ApiResult<CompletionV1> result = null;
            List<CompletionV1> partials = new List<CompletionV1>();
            bool isComplete = false;

            CompletionRequestV1 req = new CompletionRequestV1() { prompt = "hello", n = 8 };
            yield return api.Engines.Engine("ada").Completions.CreateCoroutine_EventStream(
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
            test.DestroySelf();
        }

        [UnityTest]
        public IEnumerator SearchSearchCoroutine()
        {
            string key = UTTestAuth.GetAndValidateAuthKey();
            OpenAiApiV1 api = new OpenAiApiV1(key);

            EmptyMono test = new GameObject("test", typeof(EmptyMono)).GetComponent<EmptyMono>();

            ApiResult<SearchListV1> result = null;
            SearchRequestV1 req = new SearchRequestV1() { documents = new string[] { "doc1", "doc2" }, query = "is this a doc"};
            yield return api.Engines.Engine("ada").Search.SearchCoroutine(test, req, (r) => result = r);

            Assert.IsNotNull(result);
            Assert.That(result.IsSuccess);
            Assert.IsNotNull(result.Result);
            Assert.IsNotEmpty(result.Result.data);
            Assert.That(result.Result.data.Length == 2);
            test.DestroySelf();
        }
    }
}