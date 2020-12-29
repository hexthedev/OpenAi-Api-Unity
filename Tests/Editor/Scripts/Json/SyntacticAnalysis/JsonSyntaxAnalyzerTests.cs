using NUnit.Framework;

using OpenAi.Json;

using System.Collections.Generic;

namespace OpenAiApi
{
    class SyntaxAnalyzerTests
    {
        [Test]
        public void AnalyzeTestSimpleObject()
        {
            string[] syntax = new string[]
            {
                "{", "key", ":", "value", "}"
            };

            JsonObject obj = JsonSyntaxAnalyzer.Parse(syntax);

            obj.AssertRootIsObject();
            obj.NestedValues.AssertIsValidJsonObjectArray(1);

            obj.NestedValues[0].AssertValue("key", "value");
        }

        [Test]
        public void AnalyzeTestSimpleList()
        {
            string[] syntax = new string[]
            {
                "[", "li1", ",", "li2", "]"
            };

            JsonObject obj = JsonSyntaxAnalyzer.Parse(syntax);

            obj.AssertRootIsList();
            obj.AssertListWithSimpleValues(null, "li1", "li2");
        }

        [Test]
        public void AnalyzeTestListOfObjects()
        {
            string[] syntax = new string[]
            {
                "[", "{", "key0", ":", "val0", "}", ",", "{", "key1", ":", "val1", "}", "]"
            };

            JsonObject obj = JsonSyntaxAnalyzer.Parse(syntax);

            obj.AssertRootIsList();
            obj.NestedValues.AssertIsValidJsonObjectArray(2);

            for(int i = 0; i<=1; i++)
            {
                // Test the object
                JsonObject listElement = obj.NestedValues[i];
                listElement.AssertListElementIsObject();
                listElement.NestedValues.AssertIsValidJsonObjectArray(1);

                // Test the objects inner key value pair
                JsonObject objectKeyValue = listElement.NestedValues[0];
                objectKeyValue.AssertValue($"key{i}", $"val{i}");
            }
        }

        [Test]
        public void AnalyzeTestWithListValue()
        {
            string[] syntax = new string[]
            {
                "{", "key1", ":", "[", "li1", ",", "li2", "]", "}"
            };

            JsonObject obj = JsonSyntaxAnalyzer.Parse(syntax);

            obj.AssertRootIsObject();
            obj.NestedValues.AssertIsValidJsonObjectArray(1);

            // Test the value with list
            JsonObject val = obj.NestedValues[0];
            val.AssertListWithSimpleValues("key1", "li1", "li2");
        }

        [Test]
        public void AnalyzeTestOpenAiExample()
        {
            string[] syntax = new string[]
            {
                "{", "id", ":", "cmpl - uqkvlQyYK7bGYrRHQ0eXlWi7", ",", "object", ":", "text_completion", ",", "created", ":", "1589478378", ",", "model", ":", "davinci:2020-05-03", ",", "choices", ":", "[", "{", "text", ":", " there was a girl who", ",", "index", ":", "0", ",", "logprobs", ":", "null", ",", "finish_reason", ":", "length", "}", "]", "}"
            };

            JsonObject obj = JsonSyntaxAnalyzer.Parse(syntax);

            // Root
            obj.AssertRootIsObject();
            obj.NestedValues.AssertIsValidJsonObjectArray(5);

            // First few values
            obj.NestedValues[0].AssertValue("id", "cmpl - uqkvlQyYK7bGYrRHQ0eXlWi7");
            obj.NestedValues[1].AssertValue("object", "text_completion");
            obj.NestedValues[2].AssertValue("created", "1589478378");
            obj.NestedValues[3].AssertValue("model", "davinci:2020-05-03");

            // Choices list object
            JsonObject list = obj.NestedValues[4];
            list.AssertList(1);

            // The one choices object inside the list
            JsonObject choiceObject = list.NestedValues[0];
            choiceObject.AssertListElementIsObject();
            choiceObject.NestedValues.AssertIsValidJsonObjectArray(4);

            List<JsonObject> choiceObjectKVs = choiceObject.NestedValues;
            choiceObjectKVs[0].AssertValue("text", " there was a girl who");
            choiceObjectKVs[1].AssertValue("index", "0");
            choiceObjectKVs[2].AssertValue("logprobs", "null");
            choiceObjectKVs[3].AssertValue("finish_reason", "length");
        }
    }
}
