# Context
The `OpenAi Api Unity` library is a wrapper for REST API calls made to the OpenAi Api. REST API calls are HTTP requests sent directly to the OpenAI Api resource endpoints with some simple authentication. 

An example of an actual api call looks something like this:
```
https://api.openai.com/v1/engines/davinci/completions
```

You can read more about the details of these api calls using the [OpenAi Api Reference](https://beta.openai.com/docs/api-reference)

The `OpenAi Api Unity` library implements these Api calls using Unity's built-in [UnityWebRequest](https://docs.unity3d.com/ScriptReference/Networking.UnityWebRequest.html) class and a custom Json Serailizer/Deserializer. 

HTTP requests cannot occur syncronously without causing the thread to pause. This is not ideal, so all api calls are implemented as `async` functions and as Coroutines. 

# MonoBehaviours
The `OpenAiApiGatewayV1` is a prefab and `MonoBehaviour` singleton that allows any script in a Unity scene to access the api. This Monobehaviour simply controls the initalization of an `OpenAiApiV1` object, which actually houses the api logic. 

You can add an `OpenAiApiGatewayV1` to the current scene using the menu item `OpenAi > V1 > CreateGateway`

You don't have to use the `OpenAiApiGatewayV1` prefab to access the api. This is simply one way that the `OpenAiApiV1` can be initialized. A custom `MonoBehaviour` can be written easily, it just needs to create the `OpenAiApiV1` object with valid authentication args. 

## Completer
The `OpenAiApiCompleterV1` is a simpler class that automatically initalizes the `OpenAiApiV1` object with use for a single engine, and provides the `Complete()` method to allow simple completions. 

You can add an `OpenAiApiCompleterV1` to the current scene using the menu item `OpenAi > V1 > CreateCompleter`

# API Implementation
As much as possible, the syntax for performing a call using `OpenAi Api Unity` follows the same structure as the api calls found in the [OpenAi Api Reference](https://beta.openai.com/docs/api-reference). 

All Api calls start with an `OpenAiApiV1` object. 

For example:

```csharp
# this api call (In the Create Completion Section of the docs)
POST
https://api.openai.com/v1/engines/davinci/completions

{
    "prompt" : "hey",
    "max_tokens" : 8
}

# is called using the following structure 
class ApiExample : MonoBehaviour 
{
    public OpenAiApiV1 api;

    void LogCompletion()
    {
        api.Engines.Engine("davinci").Completions.CreateCompletionCoroutine(
            this, 
            new CompletionRequestV1() { prompt = "hey", max_tokens = 8},
            (result) => { Debug.Log(result.Result.choices[0].text);}
        );
    }
}
```

Lets break this down.

Each step in an api call is called a resource. `api.Engines` returns the `EnginesResource` which allows us to use any resource call related to engines. For example, the [OpenAi Api List Engines Reference](https://beta.openai.com/docs/api-reference/list-engines) shows the list engines api call. `GET
https://api.openai.com/v1/engines` which is called using `api.Engines.ListEnginesCoroutine` function.

In some cases, resources are parameters. Like in the [OpenAi Api Retrieve Engine Reference](https://beta.openai.com/docs/api-reference/retrieve-engine). In this case `GET https://api.openai.com/v1/engines/{engine_id}` the engine is a parameter. To get an Engine resource, we provide an argument: `api.Engines.Engine("<engine_id>")`

Making an api call from a resource is always implemented as an `async` function and as a Coroutine. So the api call `POST https://api.openai.com/v1/engines/{engine_id}/completions` has two functions, `api.Engines.Engine("{engine_id}").Completions.CreateCompletionCoroutine` and `api.Engines.Engine("<engine_id>").Completions.CreateCompletionAsync`.

## Async vs Coroutine functions
Async functions use `async/await` syntax to return an `ApiResult`. The signature looks like this `async Task<ApiResult<{ResultType}>> {ApiCall}Async({RequestType} request)`

Coroutine functions require a MonoBehaviour and callbacks. The basic signature looks like this `Coroutine {ApiCall}Coroutine(MonoBehaviour mono, {RequestType} request, Action<ApiResult<{ResultType}>> onResult)`. 

### Coroutine Scripting
The Coroutine implementations run the API request as a task, and check the tasks completion every frame. 

```csharp
# ExampleMono.cs
using OpenAi.Unity.V1;

using UnityEngine;

public class Example : MonoBehaviour
{
    public void DoApiCompletion()
    {
        Debug.Log("Performing Completion in Play Mode");

        OpenAiCompleterV1.Instance.Complete(
            "prompt",
            s => Debug.Log(s),
            e => Debug.LogError(e.StatusCode)
        );
    }
}
```

### Async scripts
Async implementations let you use the `async/await` style syntax to perform api calls.

```csharp
// MyEditor.cs
using OpenAi.Api.V1;
using OpenAi.Unity.V1;

using UnityEditor;

using UnityEngine;

public class MyEditor : EditorWindow
{
    [MenuItem("MyMenu/MyEditor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MyEditor));
    }

    async void OnGUI()
    {
        SOAuthArgsV1 auth = ScriptableObject.CreateInstance<SOAuthArgsV1>();
        OpenAiApiV1 api = new OpenAiApiV1(auth.ResolveAuth());

        if (api != null && GUILayout.Button("Do Completion"))
        {
            Debug.Log("Performing Completion in Editor Time");

            ApiResult<CompletionV1> comp = await api.Engines.Engine("davinci").Completions.CreateCompletionAsync(
                new CompletionRequestV1()
                {
                    prompt = "test",
                    max_tokens = 8
                }
            );

            Debug.Log(comp.IsSuccess);
            Debug.Log(comp.Result.choices[0].text);
        }
    }
}
```

## ApiResult
The `ApiResult` class is returned to encapsulate any exception that occurs during the request. There are many reasons api requests might fail, so the `ApiResult` exists so that any error can be handled, or at the very least a successful request can be verified. 

## Extra Notes
* The [Create Completion](https://beta.openai.com/docs/api-reference/create-completion) has a `stream` parameters that changes the way the completion result is received. This required a different implementation for stream and non-stream calls. As such, there are separate functions for the stream and non-stream version of completion. Calling `api.Engines.Engine("{engine_id}").Completions.CreateCompletionCoroutine` will automatically set `stream=false` and `api.Engines.Engine("{engine_id}").Completions.CreateCompletionCoroutine_EventStream` will automatically set `stream=true` no matter the input.
