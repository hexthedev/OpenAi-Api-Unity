using NUnit.Framework;

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

            JsonObject obj = SyntaxAnalyzer.Parse(syntax);

            obj.AssertIsObjectRoot();
            obj.NestedValue.AssertIsValidJsonObjectArray(1);

            obj.NestedValue[0].AssertValue("key", "value");
        }

        [Test]
        public void AnalyzeTestSimpleList()
        {
            string[] syntax = new string[]
            {
                "[", "li1", ",", "li2", "]"
            };

            JsonObject obj = SyntaxAnalyzer.Parse(syntax);

            obj.AssertIsListRoot();
            obj.AssertValueList(null, "li1", "li2");
        }

        [Test]
        public void AnalyzeTestListOfObjects()
        {
            string[] syntax = new string[]
            {
                "[", "{", "key0", ":", "val0", "}", ",", "{", "key1", ":", "val1", "}", "]"
            };

            JsonObject obj = SyntaxAnalyzer.Parse(syntax);

            obj.AssertIsListRoot();
            obj.NestedValue.AssertIsValidJsonObjectArray(2);

            for(int i = 0; i<=1; i++)
            {
                // Test the object
                JsonObject listElement = obj.NestedValue[i];
                listElement.AssertListObject();
                listElement.NestedValue.AssertIsValidJsonObjectArray(1);

                // Test the objects inner key value pair
                JsonObject objectKeyValue = listElement.NestedValue[0];
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

            JsonObject obj = SyntaxAnalyzer.Parse(syntax);

            obj.AssertIsObjectRoot();
            obj.NestedValue.AssertIsValidJsonObjectArray(1);

            // Test the value with list
            JsonObject val = obj.NestedValue[0];
            val.AssertValueList("key1", "li1", "li2");
        }

        [Test]
        public void AnalyzeTestOpenAiExample()
        {
            string[] syntax = new string[]
            {
                "{", "id", ":", "cmpl - uqkvlQyYK7bGYrRHQ0eXlWi7", ",", "object", ":", "text_completion", ",", "created", ":", "1589478378", ",", "model", ":", "davinci:2020-05-03", ",", "choices", ":", "[", "{", "text", ":", " there was a girl who", ",", "index", ":", "0", ",", "logprobs", ":", "null", ",", "finish_reason", ":", "length", "}", "]", "}"
            };

            JsonObject obj = SyntaxAnalyzer.Parse(syntax);
        }
    }
}
