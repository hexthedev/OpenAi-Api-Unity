* Implement Chat Completion, see Issue #57

OpenAI API Changes for Chat Completion

Changes to Existing Code
- Runtime\Scenes\Api\Utility\EngineNames\EEngineName.cs: Added new model enumeration gpt_3_5_turbo
- Runtime\Scenes\Api\Utility\EngineNames\UTEngineNames.cs: Added new model enumeration gpt_3_5_turbo
- Runtime\Scripts\Api\V1\OpenAiApiV1.cs: Added ChatCompletions resource
- Editor\Scripts\Unity\V1\EMPrefabs.cs: Added editor menu OpenAi/V1/CreateChatCompleter

Additions

- Editor\Scripts\Examples\EMExampleChatRuntimeScene.cs
- Runtime\Prefabs\OpenAiChatCompleterV1.prefab
- Runtime\Scenes\ExampleChatRuntimeScene.unity
- Runtime\Scripts\Api\V1\Models\ChatCompletionV1.cs
- Runtime\Scripts\Api\V1\Models\UsageV1.cs
- Runtime\Scripts\Api\V1\Models\ChatChoiceV1.cs
- Runtime\Scripts\Api\V1\Models\ChatCompletionV1.cs
- Runtime\Scripts\Api\V1\Models\MessageV1.cs
- Runtime\Scripts\Api\V1\Api\Engines\ChatCompletion\ChatCompletionRequestV1.cs
- Runtime\Scripts\Api\V1\Api\Engines\ChatCompletion\ChatCompletionResourceV1.cs
- Runtime\Scripts\Examples\ExampleChatRuntime.cs
- Runtime\Scripts\Unity\V1\ChatCompleter\OpenAiChatCompleterV1.cs
- Runtime\Scripts\Unity\V1\ChatCompleter\SOChatCompletionArgsV1.cs
- Runtime\Config\DefaultChatCompletionArgs.asset
* Added unit tests for chat completion #58

Tests were based on tests covering existing prompt completion. Also fixed an issue with empty JSON objects and added unit tests.  The last record of a chat stream has a finish reason and an empty delta object.
* Refactored OpenAiApiV1.ChatCompletions into OpenAiApiV1.Chat.Completions

Also created distinct enumeration type EChatModelNames and removed gtp-3.5-turbo out of EEngineNames. Did not include gpt-4 models as these are still in beta.
* Documentation for chat/completions and separated DeltaV1 from MessageV1

Added some documentation for chat/completions and separated DeltaV1 from MessageV1 as role is not optional for messages but is for message deltas. Also need to see message role in Unity Inspector.
