using OpenAi.Api.Unity;
using OpenAi.Api.Unity.V1;
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
    }
}