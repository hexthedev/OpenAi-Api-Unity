using OpenAi.Unity;
using OpenAi.Unity.V1;
using OpenAi.Api.V1;

using UnityEngine;

namespace OpenAi.Api.Test
{
    public class TestManager : AMonoSingleton<TestManager>
    {
        private OpenAiApiGatewayV1 _apiGateway = null;

        public OpenAiApiV1 CleanAndProvideApi()
        {
            if(_apiGateway != null)
            {
                Destroy(_apiGateway.gameObject);
            }
            
            _apiGateway = OpenAiApiGatewayV1.Instance;
            _apiGateway.Auth = ScriptableObject.CreateInstance<SOAuthArgsV1>();
            _apiGateway.Auth.AuthType = SOAuthArgsV1.EAuthProvisionMethod.LocalFile;
            _apiGateway.InitializeApi();

            return _apiGateway.Api;
        }

        public void LogTest(string testDescription, bool result)
        {
            if (result)
            {
                Debug.Log($"[SUCCESS] {testDescription}");
            }
            else
            {
                Debug.Log($"[FAIL] {testDescription}");
            }
        }

        public bool TestApiResultSuccess(ApiResult result)
        {
            bool resultIsNotNull = result != null;
            LogTest("Result is not null", resultIsNotNull);
            if (!resultIsNotNull) return false;

            bool resultIsSuccess = result.IsSuccess;
            LogTest("Result is success", resultIsSuccess);
            return resultIsNotNull && resultIsSuccess;
        }

        public bool TestApiResultHasResponse<T>(ApiResult<T> result)
        {
            if (!TestApiResultSuccess(result)) return false;

            bool resultDataIsNotNull = result.Result != null;
            LogTest("Result data is not null", resultDataIsNotNull);
            return resultDataIsNotNull;
        }
    }
}