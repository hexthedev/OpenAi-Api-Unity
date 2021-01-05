using NUnit.Framework;

using OpenAi.Api.V1;
using System.Collections;
using UnityEngine.TestTools;

namespace OpenAi.Api.Test
{
    public class V1BugTests
    {
        [UnityTest]
        // Issue: https://github.com/hexthedev/OpenAi-Api-Unity/issues/7
        // Prompts weren't working with escape characters
        public IEnumerator Issue7_EscapeCharacterBug()
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
        // Issue: https://github.com/hexthedev/OpenAi-Api-Unity/issues/13
        // Prompts weren't working with escape characters
        public IEnumerator Issue13_MultilineStringBug()
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