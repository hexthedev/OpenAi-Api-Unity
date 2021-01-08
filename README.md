# OpenAi Api Unity
A Simple OpenAI API wrapper for Unity 

This is a community library. I am not officially affiliated with OpenAi.

Big shout out to:
* [@OkGoDoIt](https://github.com/OkGoDoIt): This code base is heavily based on the [OpenAI-API-dotnet Repo](https://github.com/OkGoDoIt/OpenAI-API-dotnet), which is a dotnet wrapper for the OpenAI Api
* [@ivomarel](https://github.com/ivomarel): For the [OpenAI_Unity Repo](https://github.com/hexthedev/OpenAI_Unity)

To report bugs, problems, suggestions please submit [Github Issues](https://github.com/hexthedev/OpenAi-Api-Unity/issues)

If anyone want to contribute, [Pull Requests](https://github.com/hexthedev/OpenAi-Api-Unity/pulls) are welcome

## Overview
This is a simple OpenAI API wrapper that implements the api calls found in the [OpenAI Api Api Reference](https://beta.openai.com/docs/api-reference) as Coroutines and Async functions. 

The intention is that the syntax follows the docs as closely as possible. For example, the api call Create Completion at the endpoint `https://api.openai.com/v1/engines/{engine_id}/completions` is called using `OpenAiApiV1.Engines.Engine("engine_id}").Completions.CreateCompletionCoroutine`

To learn more:
1. Read the Quick Start section below to see a basic example of how to use the wrapper
2. Refer to the [Documentation](https://github.com/hexthedev/OpenAi-Api-Unity/tree/main/Documentation) for a more detailed explanation of the library

### A Word on Testing
I've tested the list below. Testing for all other usecases will come with time, if I still decide to use OpenAI Beta API in Unity for projects
* Editor scripts and editor windows using async versions of API calls
* Coroutine api calls in Play Mode
  * I have not tested builds, but should work since it's really just Native C#. Any issues will likely be platform related. 
* Unit Tested basic usecases and any issue I found along the way, to ensure stability
* Only tested on a Windows machine. If Linux/Mac authentication dosen't work as expected, please let me know. 

# Quick Start

## Install

**Unity Package Manager (Recommended):**
Go to the Unity Package Manager (`Window > Package Manager`), and click the `+` icon in the top left hand corner. Choose `Add package from git URL...` and provide the url `https://github.com/hexthedev/OpenAi-Api-Unity`.

**Unity Package:**
Go to https://github.com/hexthedev/OpenAi-Api-Unity/releases and download the desired release. once downloaded, open the file and follow the instructions to import it into Unity. 

**Git Submodule**:
For more advanced git users, you can simply add this repo as a submodule in your assets folder. This is especially useful if you want to edit, change and version the `OpenAi Api Unity` code.

## Authenticate
Add a file to `~/.openai/auth.json` (Linux/Mac) or `%USERPROFILE%/.openai/auth.json` (Windows)

```json
// auth.json
{
  "private_api_key":"<YOUR_KEY>",
  (optional) "organization":"<YOUR_ORGANIZATION_ID>"
}
```

## Add Singleton to Scene
Add the `OpenAiCompleterV1` prefab to your scene. Located at `<PATH_TO_PACKAGE>/Runtime/Prefabs`

## Make Completion
Make a completion
```csharp
OpenAiCompleterV1.Instance.Complete(
  "hey", 
  (r) => {Debug.Log(r); }, 
  (e) => Debug.LogError($"OpenAi Api Completion Error: StatusCode: {e.StatusCode}")
);
```

The completion will take some time, since it's a request response to a server.

# What Next
The above quick start is an extremly simple way to use the `OpenAi Api Unity` library. For more advanced use cases, refer to the [OpenAi Api Unity Documentation](https://github.com/hexthedev/OpenAi-Api-Unity/tree/main/Documentation)