# Context
The `OpenAi Api Unity` library is a wrapper for the OpenAI Api.

The following technical decisions were made during development:
* All json serialization and deserialization is performed with a custom serializer so that the library has no dependencies
* Classes and objects were created to make the libraries api call syntax as close to the api calls shown in the [OpenAi Api Reference](https://beta.openai.com/docs/api-reference)
* All Api calls are implemented in two ways, as async functions and as Coroutines.

## Getting Started
All api calls are accessible through the `OpenAiApiV1` object. When created, it requires initialization with a `SAuthArgsV1` which provides the authentication keys. For details [See Authentication](https://github.com/hexthedev/OpenAi-Api-Unity/blob/main/Documentation/2_Authentication.md)

`OpenAiApiV1` is not a `MonoBehaviour`, so it needs to be initalized somewhere to be used in a scene. The `OpenAiApiGatewayV1` prefab is a singleton that handles the initalization of the `OpenAiApiV1` automatically during the `Start()` method. This can be added via the menu `OpenAi > V1 > CreateGateway`. You can also disable the automatic initalization on `Start()`, and instead initalize the `OpenAiApiV1` manually. The `OpenAiApiV1` object is accessed through `OpenAiApiGatewayV1.Instance.Api`. For details [See ApiCalls](https://github.com/hexthedev/OpenAi-Api-Unity/blob/main/Documentation/3_ApiCalls.md)

Once you're able to access the `OpenAiApiV1` object, all api calls follow the [OpenAi Api Reference](https://beta.openai.com/docs/api-reference) as closely as possible. For example, the api call `https://api.openai.com/v1/engines/{engine_id}/completions` called Create Completions in the docs is called using the pattern `OpenAiApiV1Instance.Engines.Engine("<engine_id>").Completions.CreateCompletion`. For details [See ApiCalls](https://github.com/hexthedev/OpenAi-Api-Unity/blob/main/Documentation/3_ApiCalls.md)

## Serailization
A custom json serializer/deserializer was implemented to remove a dependency on Newtonsoft. This was the make the library more lightweight and to avoid some problems compiling Newtonsoft on some platforms. 

For details [See JsonSerialization](https://github.com/hexthedev/OpenAi-Api-Unity/blob/main/Documentation/4_JsonSerialization.md)
