# OpenAi Api Unity
A Simple OpenAI API wrapper for Unity 

This is a community library. I am not officially affiliated with OpenAi.

Big shout out to:
* @OkGoDoIt: This code base is heavily based on the [OpenAI-API-dotnet Repo](https://github.com/OkGoDoIt/OpenAI-API-dotnet), which is a dotnet wrapper for the OpenAI Api
* @ivomarel: For the [OpenAI_Unity Repo](https://github.com/ivomarel/OpenAI_Unity)

To report bugs, problems, suggestions please submit [Github Issues](https://github.com/ivomarel/OpenAI_Unity/issues)

If anyone want to contribute, [Pull Requests](https://github.com/ivomarel/OpenAI_Unity/pulls) are welcome

## Overview
This is a simple OpenAI API wrapper that implements the api calls found in the [OpenAI Api Api Reference](https://beta.openai.com/docs/api-reference) as Coroutines and Async functions. 

The intention is that the syntax follows the docs as closely as possible. For example, the api call Create Completion at the endpoint `https://api.openai.com/v1/engines/{engine_id}/completions` is called using `OpenAiApiV1.Engines.Engine("engine_id").Completions.CreateCompletionCoroutine()`. As the api is expanded by OpenAi, I will continue to follow OpenAi's api structure.




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




This wrapper has been designed to follow the [OpenAI API Reference](https://beta.openai.com/docs/api-reference) as closely as possible in syntax. 

Save your [Secret API Key](https://beta.openai.com/docs/developer-quickstart) as the file `~/.openai/auth.json` (Linux/Mac) or `%USERPROFILE%/.openai/auth.json` (Windows)

TO DO: Add the mono behviour

TO DO: Make a call to the api


## Docs

### Authentication
The OpenAi Api is currently in beta and requires special access to use it. You can [Sign Up for Access](https://openai.com/)

Access to the API is managed using your [Secret API Key](https://beta.openai.com/docs/developer-quickstart)

You have two options to authenticate the api in Unity. 
  1. Copy/Paste in Scriptable Object (Not recommended for projects commited to public repos, as this will reveal your key to the public)
  2. Save in `auth.json` file

TO DO: Sort this out in the library

