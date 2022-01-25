**WARNING**: I'm currently not in a position to update this repo and keep up with changes to the OpenAI Api. From my end, I doubt updates will come. However, I will gladly accept pull requests to fix any problems in the code base. If anyone is interested in being a more active maintainer of the repo, please let me know. 

# OpenAi Api Unity
A simple OpenAI API wrapper for Unity 

This is a community library. I am not officially affiliated with OpenAi.

Big shout out to:
* [@OkGoDoIt](https://github.com/OkGoDoIt): This code base is heavily based on the [OpenAI-API-dotnet Repo](https://github.com/OkGoDoIt/OpenAI-API-dotnet), which is a dotnet wrapper for the OpenAI Api
* [@ivomarel](https://github.com/ivomarel): For the [OpenAI_Unity Repo](https://github.com/hexthedev/OpenAI_Unity)

To report bugs, problems, suggestions please submit [Github Issues](https://github.com/hexthedev/OpenAi-Api-Unity/issues)

If anyone wants to contribute, [Pull Requests](https://github.com/hexthedev/OpenAi-Api-Unity/pulls) are welcome

## Status
| Api Call | Implemented | Bare-Minimum Tests | Thourough Tests | 
| --- | --- | --- | --- |
| `GET /engines` | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark: |
| `GET /engines/{engine_id}` | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark: |
| `POST /engines/{engine_id}/completions` | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark: |
| `GET /engines/{engine_id}/completions/browser_stream` | :x: | :heavy_minus_sign: | :heavy_minus_sign: |
| `POST /engines/{engine_id}/search` | :heavy_check_mark: | :heavy_check_mark: | :heavy_minus_sign: |
| `POST /classifications` | :heavy_check_mark: | :heavy_check_mark: | :heavy_minus_sign: |
| `POST /answers` | :heavy_check_mark: | :heavy_check_mark: | :heavy_minus_sign: |
| `GET /files` | :heavy_check_mark: | :heavy_minus_sign: | :heavy_minus_sign: |
| `POST /files/{file_id}` | :x: | :heavy_minus_sign: | :heavy_minus_sign: |
| `GET /files/{file_id}` | :heavy_check_mark: | :heavy_minus_sign: | :heavy_minus_sign: |

## Overview
This is a simple OpenAI API wrapper that implements the API calls found in the [OpenAI Api Reference](https://beta.openai.com/docs/api-reference) as Coroutines and Async functions. 

The syntax follows the docs as closely as possible. For example, the API call Create Completion at the endpoint `https://api.openai.com/v1/engines/{engine_id}/completions` is called using `OpenAiApiV1.Engines.Engine("<engine_id>").Completions.CreateCompletionCoroutine`

To learn more:
1. Read the Quick Start section below to see a basic example of how to use the wrapper
2. Refer to the [Documentation](https://github.com/hexthedev/OpenAi-Api-Unity/tree/main/Documentation) for a more detailed explanation of the library

### What is and isn't tested
I've tested the list below. Testing for all other usecases will come with time
* Editor scripts and editor windows using async versions of API calls
* Coroutine api calls in Play Mode
  * I have not tested builds, but should work since it's really just Native C#. Any issues will likely be platform related. 
* Unit Tested basic usecases and any issue I found along the way, to ensure stability
* Only tested on a Windows machine. If Linux/Mac authentication dosen't work as expected, please let me know. 
* I do not have an orgnaization, I have not been able to test the organization key functionality during authentication

# Quick Start

See video: https://youtu.be/Ju-i0sxsX7E

## Install

**Unity Package Manager (Recommended):**
Go to the Unity Package Manager (`Window > Package Manager`), and click the `+` icon in the top left hand corner. Choose `Add package from git URL...` and provide the url `https://github.com/hexthedev/OpenAi-Api-Unity.git`.

**Unity Package:**
Go to https://github.com/hexthedev/OpenAi-Api-Unity/releases and download the desired release. once downloaded, open the file and follow the instructions to import it into Unity. 

**Git Submodule**:
For more advanced git users, you can simply add this repo as a submodule in your assets folder. This is especially useful if you want to edit, change and version the `OpenAi Api Unity` code.

## Authenticate
Add a file to the path `~/.openai/auth.json` (Linux/Mac) or `%USERPROFILE%/.openai/auth.json` (Windows)

if you only have an API key, the `auth.json` should look like this
```json
{
  "private_api_key":"<YOUR_KEY>"
}
```

If you have an orgnaization key, the `auth.json` should look like this
```json
{
  "private_api_key":"<YOUR_KEY>",
  "organization":"<YOUR_ORGANIZATION_ID>"
}
```

## Editor Script
To see an example of a completion in an editor script:
  * From the top bar to to `OpenAi > Examples > Completion In Editor Window`.
  * To see the code, Click the `Code` reference at the top of the window. 

## Play Script
To see an example of a completion at Runtime:
  * If you're working on a scene, save the scene you are working on
  * From the top bar click `OpenAi > Examples > Completion At Runtime`
  * Look at the `CompletionExample` object in the hierarchy and check out the code in `ExampleOpenAiApiRuntime`
  * Press play and run the scene

# What Next
The above quick start is an extremly simple way to use the `OpenAi Api Unity` library. For more advanced use cases, refer to the [OpenAi Api Unity Documentation](https://github.com/hexthedev/OpenAi-Api-Unity/tree/main/Documentation)
