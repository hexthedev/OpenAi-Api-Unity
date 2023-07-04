﻿using NUnit.Framework;

using OpenAi.Api.V1;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.TestTools;

namespace OpenAi.Api.Test
{
    public class V1PlayTests
    {
        private TestManager test;
        private OpenAiApiV1 api;

        [OneTimeSetUp]
        public void OneTimeSetup() => test = TestManager.Instance;

        [SetUp]
        public void SetUp() => api = test.CleanAndProvideApi();

        #region Engines Requests
        [UnityTest]
        public IEnumerator EnginesListCoroutine()
        {
            ApiResult<EnginesListV1> result = null;
            yield return api.Engines.ListEnginesCoroutine(test, (r) => result = r);

            if (!test.TestApiResultHasResponse(result)) Assert.That(false);

            bool isDataNotEmpty = result.Result.data != null;
            test.LogTest("Data is not empty", isDataNotEmpty);

            bool containsAda = false;
            foreach (EngineV1 engine in result.Result.data)
            {
                if (engine.id == "ada")
                {
                    containsAda = true;
                    break;
                }
            }
            test.LogTest("Engine id contains \"ada\"", containsAda);

            Assert.That(isDataNotEmpty && containsAda);
        }

        [UnityTest]
        public IEnumerator EnginesListAsync()
        {
            Task<ApiResult<EnginesListV1>> resTask = api.Engines.ListEnginesAsync();

            while (!resTask.IsCompleted) yield return new WaitForEndOfFrame();

            ApiResult<EnginesListV1> res = resTask.Result;

            if (!test.TestApiResultHasResponse(res)) Assert.That(false);

            bool isResultDataNotEmpty = res.Result.data != null && res.Result.data.Length > 0;
            test.LogTest("Result data is not empty", isResultDataNotEmpty);

            Assert.That(isResultDataNotEmpty);
        }
        #endregion

        #region Engine Requests
        [UnityTest]
        public IEnumerator EngineRetrieveCoroutine()
        {
            ApiResult<EngineV1> result = null;
            yield return api.Engines.Engine("ada").RetrieveEngineCoroutine(test, (r) => result = r);

            if (!test.TestApiResultHasResponse(result)) Assert.That(false);

            bool isResultIdAda = result.Result.id == "ada";
            test.LogTest("The result id is ada", isResultIdAda);

            Assert.That(isResultIdAda);
        }

        [UnityTest]
        public IEnumerator EngineRetrieveAsync()
        {
            Task<ApiResult<EngineV1>> resultTask = api.Engines.Engine("ada").RetrieveEngineAsync();

            while (!resultTask.IsCompleted) yield return new WaitForEndOfFrame();

            ApiResult<EngineV1> result = resultTask.Result;

            if (!test.TestApiResultHasResponse(result)) Assert.That(false);

            bool isResultIdAda = result.Result.id == "ada";
            test.LogTest("The result id is ada", isResultIdAda);

            Assert.That(isResultIdAda);
        }
        #endregion

        #region Completion Requests
        [UnityTest]
        public IEnumerator Completions_TestAllRequestParamsString()
        {
            ApiResult<CompletionV1> result = null;

            CompletionRequestV1 req = new CompletionRequestV1()
            {
                prompt = "hello",
                best_of = 1,
                echo = false,
                frequency_penalty = 0,
                presence_penalty = 0,
                logit_bias = new Dictionary<string, int>() { { "123", -100 }, { "111", 100 } },
                stop = "###",
                logprobs = 0,
                stream = false,
                max_tokens = 8,
                n = 1,
                temperature = 0,
                top_p = 1
            };

            yield return api.Engines.Engine("ada").Completions.CreateCompletionCoroutine(test, req, (r) => result = r);

            if (!test.TestApiResultHasResponse(result)) Assert.That(false);

            bool doesResultObjectExist = result.Result.choices != null && result.Result.choices.Length > 0;
            test.LogTest("Does non empty result object exist", doesResultObjectExist);

            Assert.That(doesResultObjectExist);
        }

        [UnityTest]
        public IEnumerator Completions_TestAllRequestParamsArray()
        {
            ApiResult<CompletionV1> result = null;

            CompletionRequestV1 req = new CompletionRequestV1()
            {
                prompt = new string[] { "prompt1", "prompt2" },
                best_of = 1,
                echo = false,
                frequency_penalty = 0,
                presence_penalty = 0,
                logit_bias = new Dictionary<string, int>() { { "123", -100 }, { "111", 100 } },
                stop = new string[] { "stop1", "stop2" },
                logprobs = 0,
                stream = false,
                max_tokens = 8,
                n = 1,
                temperature = 0,
                top_p = 1
            };

            yield return api.Engines.Engine("ada").Completions.CreateCompletionCoroutine(test, req, (r) => result = r);

            if (!test.TestApiResultHasResponse(result)) Assert.That(false);

            bool doesResultObjectExist = result.Result.choices != null && result.Result.choices.Length > 0;
            test.LogTest("Does non empty result object exist", doesResultObjectExist);

            Assert.That(doesResultObjectExist);
        }

        [UnityTest]
        public IEnumerator CompletionsCreateCoroutine()
        {
            ApiResult<CompletionV1> result = null;
            CompletionRequestV1 req = new CompletionRequestV1() { prompt = "hello", n = 8 };
            yield return api.Engines.Engine("ada").Completions.CreateCompletionCoroutine(test, req, (r) => result = r);

            if (!test.TestApiResultHasResponse(result)) Assert.That(false);
            bool doesResultObjectExist = result.Result.choices != null && result.Result.choices.Length > 0;
            test.LogTest("Does non empty result object exist", doesResultObjectExist);

            Assert.That(doesResultObjectExist);
        }

        [UnityTest]
        public IEnumerator CompletionsCreateAsync()
        {
            Task<ApiResult<CompletionV1>> resTask = api.Engines.Engine("ada").Completions.CreateCompletionAsync(
                new CompletionRequestV1() { prompt = "hello", max_tokens = 8 }
            );

            while (!resTask.IsCompleted) yield return new WaitForEndOfFrame();

            ApiResult<CompletionV1> res = resTask.Result;

            Assert.That(test.TestApiResultHasResponse(res));
        }

        [UnityTest]
        public IEnumerator CompletionsCreateCoroutine_EventStream()
        {
            ApiResult<CompletionV1> result = null;
            List<CompletionV1> partials = new List<CompletionV1>();
            bool isComplete = false;

            CompletionRequestV1 req = new CompletionRequestV1() { prompt = "hello", n = 8 };
            yield return api.Engines.Engine("ada").Completions.CreateCompletionCoroutine_EventStream(
                test,
                req,
                (r) => result = r,
                (i, l) => partials.Add(l),
                () => isComplete = true
            );

            float timer = 10f;
            while (!isComplete && timer > 0)
            {
                timer -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            test.LogTest("Stream was completed", isComplete);

            if (!test.TestApiResultSuccess(result)) Assert.That(false);

            bool partialsNotEmpty = partials != null && partials.Count > 0;
            test.LogTest("Partial reponses were received", partialsNotEmpty);

            Assert.That(isComplete && partialsNotEmpty);
        }

        [UnityTest]
        public IEnumerator CompletionsCreateAsync_EventStream()
        {
            ApiResult<CompletionV1> result = null;
            List<CompletionV1> completions = new List<CompletionV1>();
            bool isComplete = false;

            Task engineTask = api.Engines.Engine("davinci").Completions.CreateCompletionAsync_EventStream(
                new CompletionRequestV1() { prompt = "hello", max_tokens = 8, stream = true },
                (r) => result = r,
                (i, c) => completions.Add(c),
                () => isComplete = true
            );

            while (!engineTask.IsCompleted) yield return new WaitForEndOfFrame();

            test.LogTest("Stream was completed", isComplete);

            if (!test.TestApiResultSuccess(result)) Assert.That(false);

            bool completionsNotEmpty = completions != null && completions.Count > 0;
            test.LogTest("Partial reponses were received", completionsNotEmpty);

            Assert.That(isComplete && completionsNotEmpty);
        }
        #endregion

        #region Chat Completion Requests

        [UnityTest]
        public IEnumerator ChatCompletions_TestAllRequestParamsString()
        {
            ApiResult<ChatCompletionV1> result = null;

            MessageV1 message = new MessageV1();
            message.role = MessageV1.MessageRole.user;
            message.content = "hello";

            List<MessageV1> messages = new List<MessageV1>();
            messages.Add(message);

            ChatCompletionRequestV1 req = new ChatCompletionRequestV1()
            {
                model = "gpt-3.5-turbo",
                messages = messages,
                frequency_penalty = 0,
                presence_penalty = 0,
                logit_bias = new Dictionary<string, int>() { { "123", -100 }, { "111", 100 } },
                stop = "###",
                stream = false,
                max_tokens = 8,
                n = 1,
                temperature = 0,
                top_p = 1,
                user = ""
            };

            yield return api.Chat.Completions.CreateChatCompletionCoroutine(test, req, (r) => result = r);

            if (!test.TestApiResultHasResponse(result)) Assert.That(false);

            bool doesResultObjectExist = result.Result.choices != null && result.Result.choices.Length > 0;
            test.LogTest("Does non empty result object exist", doesResultObjectExist);

            Assert.That(doesResultObjectExist);
        }

        [UnityTest]
        public IEnumerator ChatCompletions_TestAllRequestParamsArray()
        {
            ApiResult<ChatCompletionV1> result = null;

            MessageV1 message = new MessageV1();
            message.role = MessageV1.MessageRole.user;
            message.content = "hello";

            List<MessageV1> messages = new List<MessageV1>();
            messages.Add(message);

            ChatCompletionRequestV1 req = new ChatCompletionRequestV1()
            {
                model = "gpt-3.5-turbo",
                messages = messages,
                frequency_penalty = 0,
                presence_penalty = 0,
                logit_bias = new Dictionary<string, int>() { { "123", -100 }, { "111", 100 } },
                stop = new string[] { "stop1", "stop2" },
                stream = false,
                max_tokens = 8,
                n = 1,
                temperature = 0,
                top_p = 1,
                user = ""
            };

            yield return api.Chat.Completions.CreateChatCompletionCoroutine(test, req, (r) => result = r);

            if (!test.TestApiResultHasResponse(result)) Assert.That(false);

            bool doesResultObjectExist = result.Result.choices != null && result.Result.choices.Length > 0;
            test.LogTest("Does non empty result object exist", doesResultObjectExist);

            Assert.That(doesResultObjectExist);
        }

        [UnityTest]
        public IEnumerator ChatCompletionsCreateCoroutine()
        {
            ApiResult<ChatCompletionV1> result = null;

            MessageV1 message = new MessageV1();
            message.role = MessageV1.MessageRole.user;
            message.content = "hello";

            List<MessageV1> messages = new List<MessageV1>();
            messages.Add(message);

            ChatCompletionRequestV1 req = new ChatCompletionRequestV1()
            {
                model = "gpt-3.5-turbo",
                messages = messages,
                n = 8
            };

            yield return api.Chat.Completions.CreateChatCompletionCoroutine(test, req, (r) => result = r);

            if (!test.TestApiResultHasResponse(result)) Assert.That(false);
            bool doesResultObjectExist = result.Result.choices != null && result.Result.choices.Length > 0;
            test.LogTest("Does non empty result object exist", doesResultObjectExist);

            Assert.That(doesResultObjectExist);
        }

        [UnityTest]
        public IEnumerator ChatCompletionsCreateAsync()
        {
            MessageV1 message = new MessageV1();
            message.role = MessageV1.MessageRole.user;
            message.content = "hello";

            List<MessageV1> messages = new List<MessageV1>();
            messages.Add(message);

            ChatCompletionRequestV1 req = new ChatCompletionRequestV1()
            {
                model = "gpt-3.5-turbo",
                messages = messages,
                max_tokens = 8
            };

            Task<ApiResult<ChatCompletionV1>> resTask = api.Chat.Completions.CreateChatCompletionAsync(req);

            while (!resTask.IsCompleted) yield return new WaitForEndOfFrame();

            ApiResult<ChatCompletionV1> res = resTask.Result;

            Assert.That(test.TestApiResultHasResponse(res));
        }

        [UnityTest]
        public IEnumerator ChatCompletionsCreateCoroutine_EventStream()
        {
            ApiResult<ChatCompletionV1> result = null;
            List<ChatCompletionV1> partials = new List<ChatCompletionV1>();
            bool isComplete = false;

            MessageV1 message = new MessageV1();
            message.role = MessageV1.MessageRole.user;
            message.content = "hello";

            List<MessageV1> messages = new List<MessageV1>();
            messages.Add(message);

            ChatCompletionRequestV1 req = new ChatCompletionRequestV1()
            {
                model = "gpt-3.5-turbo",
                messages = messages,
                max_tokens = 8
            };

            yield return api.Chat.Completions.CreateChatCompletionCoroutine_EventStream(
                test,
                req,
                (r) => result = r,
                (i, l) => partials.Add(l),
                () => isComplete = true
            );

            float timer = 10f;
            while (!isComplete && timer > 0)
            {
                timer -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            test.LogTest("Stream was completed", isComplete);

            if (!test.TestApiResultSuccess(result)) Assert.That(false);

            bool partialsNotEmpty = partials != null && partials.Count > 0;
            test.LogTest("Partial reponses were received", partialsNotEmpty);

            Assert.That(isComplete && partialsNotEmpty);
        }

        [UnityTest]
        public IEnumerator ChatCompletionsCreateAsync_EventStream()
        {
            ApiResult<ChatCompletionV1> result = null;
            List<ChatCompletionV1> completions = new List<ChatCompletionV1>();
            bool isComplete = false;

            MessageV1 message = new MessageV1();
            message.role = MessageV1.MessageRole.user;
            message.content = "hello";

            List<MessageV1> messages = new List<MessageV1>();
            messages.Add(message);

            ChatCompletionRequestV1 req = new ChatCompletionRequestV1()
            {
                model = "gpt-3.5-turbo",
                messages = messages,
                max_tokens = 8,
                stream = true
            };

            Task engineTask = api.Chat.Completions.CreateChatCompletionAsync_EventStream(
                req,
                (r) => result = r,
                (i, c) => completions.Add(c),
                 () => isComplete = true
            );

            while (!engineTask.IsCompleted) yield return new WaitForEndOfFrame();

            test.LogTest("Stream was completed", isComplete);

            if (!test.TestApiResultSuccess(result)) Assert.That(false);

            bool completionsNotEmpty = completions != null && completions.Count > 0;
            test.LogTest("Partial reponses were received", completionsNotEmpty);

            Assert.That(isComplete && completionsNotEmpty);
        }

        [UnityTest]
        public IEnumerator ChatCompletionsCreateAsync_Multiple()
        {
            MessageV1 message = new MessageV1();
            message.role = MessageV1.MessageRole.system;
            message.content = "You are Yoda from Star Wars.";

            List<MessageV1> messages = new List<MessageV1>();
            messages.Add(message);

            message = new MessageV1();
            message.role = MessageV1.MessageRole.user;
            message.content = "Is Vader good or evil?";
            messages.Add(message);

            ChatCompletionRequestV1 req = new ChatCompletionRequestV1()
            {
                model = "gpt-3.5-turbo",
                messages = messages
            };

            Task<ApiResult<ChatCompletionV1>> resTask = api.Chat.Completions.CreateChatCompletionAsync(req);

            while (!resTask.IsCompleted) yield return new WaitForEndOfFrame();

            ApiResult<ChatCompletionV1> res = resTask.Result;

            Assert.That(test.TestApiResultHasResponse(res));
        }
        #endregion

        #region Images generations
        [UnityTest]
        public IEnumerator ImagesGenerationsCoroutine_Url()
        {
            ApiResult<ImagesGenerationsV1> result = null;
            var req = new ImagesGenerationsRequestV1()
            {
                prompt = "OpenAI images generations running inside Unity game engine",
                n = 2,
                size = ExtensionMethods.IMAGE_SIZE.imsize_256x256,
                response_format = ExtensionMethods.IMAGE_RESPONSE_FORMAT.url
            };

            yield return api.Images.Generations.CreateImagesGenerationCoroutine(test, req, r => result = r);

            if (!test.TestApiResultHasResponse(result)) Assert.That(false);

            // 2 images urls
            Assert.That(result.Result.data != null && result.Result.data.Length == 2);
            Assert.That(result.Result.data[0].url.ToLowerInvariant().StartsWith("http")
                && result.Result.data[1].url.ToLowerInvariant().StartsWith("http")
                );
        }

        [UnityTest]
        public IEnumerator ImagesGenerationsAsync_Url()
        {
            var req = new ImagesGenerationsRequestV1()
            {
                prompt = "OpenAI images generations running inside Unity game engine",
                n = 2,
                size = ExtensionMethods.IMAGE_SIZE.imsize_256x256,
                response_format = ExtensionMethods.IMAGE_RESPONSE_FORMAT.url
            };

            Task<ApiResult<ImagesGenerationsV1>> resTask = api.Images.Generations.CreateImagesGenerationAsync(req);

            while (!resTask.IsCompleted) yield return new WaitForEndOfFrame();

            var res = resTask.Result;
            Assert.That(test.TestApiResultHasResponse(res));
        }
        #endregion
    }
}

