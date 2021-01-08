using NUnit.Framework;

using OpenAi.Api.V1;
using OpenAi.Unity.V1;

using System.Collections;
using System.Net.Http;

using UnityEngine;
using UnityEngine.TestTools;

namespace OpenAi.Api.Test
{
    public class V1BugTests
    {
        [UnityTest]
        // Issue: https://github.com/hexthedev/OpenAi-Api-Unity/issues/7
        // Prompts weren't working with escape characters
        public IEnumerator Issue007_EscapeCharacterBug()
        {
            TestManager test = TestManager.Instance;
            OpenAiApiV1 api = test.CleanAndProvideApi();

            ApiResult<CompletionV1> result = null;
            yield return api.Engines.Engine("ada").Completions.CreateCompletionCoroutine(
                test, 
                new CompletionRequestV1() { prompt = "something\r\n", max_tokens = 8 },
                (r) => result = r
            );

            Assert.IsNotNull(result);
            Assert.That(result.IsSuccess);
            Assert.IsNotNull(result.Result);
        }

        [UnityTest]
        // Issue: https://github.com/hexthedev/OpenAi-Api-Unity/issues/10
        // For some reason the completer is logging twice
        public IEnumerator Issue010_CompleterLoggingTwice()
        {
            TestManager test = TestManager.Instance;
            OpenAiApiV1 api = test.CleanAndProvideApi();
            OpenAiCompleterV1 comp = OpenAiCompleterV1.Instance;
            yield return new WaitForEndOfFrame();
            
            int count = 0;
            string res = null;
            HttpResponseMessage err = null;
            yield return OpenAiCompleterV1.Instance.Complete(
                "test", extractRes, extractErr
            );

            int count2 = count;
            string res2 = res;
            HttpResponseMessage err2 = err;

            Assert.IsNotNull(res);
            Assert.IsNull(err);
            Assert.That(count == 1);

            void extractRes(string r)
            {
                res = r;
                count++;
            } 

            void extractErr(HttpResponseMessage e)
            {
                err = e;
                count++;
            }
        }

        [UnityTest]
        // Issue: https://github.com/hexthedev/OpenAi-Api-Unity/issues/13
        // Prompts weren't working multiline strings
        public IEnumerator Issue013_MultilineStringBug()
        {
            TestManager test = TestManager.Instance;
            OpenAiApiV1 api = test.CleanAndProvideApi();

            string multiprompt = @"test
""Are these an isse""
<how about these />
";

            ApiResult<CompletionV1> result = null;
            yield return api.Engines.Engine("ada").Completions.CreateCompletionCoroutine(
                test,
                new CompletionRequestV1() { prompt = multiprompt, max_tokens = 8 },
                (r) => result = r
            );

            Assert.IsNotNull(result);
            Assert.That(result.IsSuccess);
            Assert.IsNotNull(result.Result);
        }
    }
}