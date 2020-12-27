using NUnit.Framework;

using System.Collections;

using UnityEngine;
using UnityEngine.TestTools;

namespace OpenAiApi
{
    public class OpenAiApiV1PTests
    {
        [UnityTest]
        public IEnumerator OpenAiApiV1TestEnginesListCoroutine()
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
        }



        private class EmptyMono : MonoBehaviour
        {
            
        }

    }
}