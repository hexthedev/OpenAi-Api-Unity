# OpenAi Api Unity
A Simple OpenAI API wrapper for Unity 

This is a community library. I am not officially affiliated with OpenAi.

Big shout out to:
* [@OkGoDoIt](https://github.com/OkGoDoIt): This code base is heavily based on the [OpenAI-API-dotnet Repo](https://github.com/OkGoDoIt/OpenAI-API-dotnet), which is a dotnet wrapper for the OpenAI Api
* [@ivomarel](https://github.com/ivomarel): For the [OpenAI_Unity Repo](https://github.com/ivomarel/OpenAI_Unity)

To report bugs, problems, suggestions please submit [Github Issues](https://github.com/ivomarel/OpenAI_Unity/issues)

If anyone want to contribute, [Pull Requests](https://github.com/ivomarel/OpenAI_Unity/pulls) are welcome

## Overview
This is a simple OpenAI API wrapper that implements the api calls found in the [OpenAI Api Api Reference](https://beta.openai.com/docs/api-reference) as Coroutines and Async functions. 

The intention is that the syntax follows the docs as closely as possible. For example, the api call Create Completion at the endpoint `https://api.openai.com/v1/engines/{engine_id}/completions` is called using `OpenAiApiV1.Engines.Engine("engine_id").Completions.CreateCompletionCoroutine()`. As the api is expanded by OpenAi, I will continue to follow OpenAi's api structure.

To Learn more:
1. Read the Quick Start section below to see a basic example of how to use the wrapper
2. Refer to the [Documentation](https://github.com/hexthedev/OpenAi-Api-Unity/tree/main/Documentation) for a more detailed explanation of the library

# Quick Start

**Authenticate**:
Add a file to `~/.openai/auth.json` (Linux/Mac) or `%USERPROFILE%/.openai/auth.json` (Windows)

```json
// auth.json
{
  "private_api_key":"<YOUR_KEY>",
  (optional) "organization":"<YOUR_ORGANIZATION_ID>"
}
```

**Gateway**:
Add the `OpenAiApiGatewayV1` prefab to your scene. Located at `<PATH_TO_PACKAGE>/Runtime/Prefabs`

**Use Api**:
Reference the gateway singleton in code to access an initalized `OpenAiApiV1` instance. Use this to make api calls. Example below:

```csharp
//SomeMonoBehavior.cs
public class SomeMonoBehaviour : MonoBehaviour
{
  ...
  public void DoApiCompletion()
  {
    // gets the api object from singleton
    OpenAiApiV1 api = OpenAiApiGatewayV1.Instance.Api;

    // starts a coroutine that performs the necessary http request
    // to do a CreateCompletion command see: https://beta.openai.com/docs/api-reference/create-completion
    api.Engines.Engine("davinci").Completions.CreateCompletionCoroutine(
      this,
      new CompletionRequestV1(){
        prompt = "Sup", 
        max_tokens = 8
      },
      OnCompletionRecieved
    );
  }

  public void OnCompletionRecieved(ApiResult<CompletionV1> result)
  {
    // All requests return an api result. Check if the request succeeded
    if (result.IsSuccess)
    {
      // Extract the completion
      CompletionV1 completion = result.Result;
      
      // Log each completion to the console
      foreach(ChoiceV1 choice in completion.choices)
      {
          Debug.Log(choice.text);
      }
    }
  }
  ...
}
```