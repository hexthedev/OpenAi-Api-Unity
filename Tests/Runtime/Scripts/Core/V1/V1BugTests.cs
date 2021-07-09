using NUnit.Framework;

using OpenAi.Api.V1;
using OpenAi.Unity.V1;

using System.Collections;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TestTools;

namespace OpenAi.Api.Test
{
    public class V1BugTests
    {
        private TestManager test;
        private OpenAiApiV1 api;

        [OneTimeSetUp]
        public void OneTimeSetup() => test = TestManager.Instance;

        [SetUp]
        public void SetUp() => api = test.CleanAndProvideApi();

        [UnityTest]
        // Issue: https://github.com/hexthedev/OpenAi-Api-Unity/issues/7
        // Prompts weren't working with escape characters
        public IEnumerator Issue007_EscapeCharacterBug()
        {
            ApiResult<CompletionV1> result = null;
            yield return api.Engines.Engine("ada").Completions.CreateCompletionCoroutine(
                test, 
                new CompletionRequestV1() { prompt = "something\r\n", max_tokens = 8 },
                (r) => result = r
            );

            Assert.That(test.TestApiResultHasResponse(result));
        }

        [UnityTest]
        // Issue: https://github.com/hexthedev/OpenAi-Api-Unity/issues/10
        // For some reason the completer is logging twice
        public IEnumerator Issue010_CompleterLoggingTwice()
        {
            OpenAiCompleterV1 comp = OpenAiCompleterV1.Instance;
            yield return new WaitForEndOfFrame();
            
            int count = 0;
            string res = null;
            UnityWebRequest err = null;
            yield return OpenAiCompleterV1.Instance.Complete(
                "test", extractRes, extractErr
            );

            int count2 = count;
            string res2 = res;
            UnityWebRequest err2 = err;

            bool resIsNotNull = res != null;
            test.LogTest("A response was received by the first request", resIsNotNull);

            bool errIsNull = err == null;
            test.LogTest("The web request is null", errIsNull);

            bool only1requestHappened = count == 1;
            test.LogTest("Only 1 request happened", only1requestHappened);

            Assert.That(resIsNotNull && errIsNull && only1requestHappened);

            void extractRes(string r)
            {
                res = r;
                count++;
            } 

            void extractErr(UnityWebRequest e)
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

            Assert.That(test.TestApiResultHasResponse(result));
        }
    }
}