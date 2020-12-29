using NUnit.Framework;

using OpenAi.Json;

namespace OpenAiApi
{
    public class JsonLexerTests
    {
        [Test]
        public void LexerTestSimpleObject()
        {
            string[] lex = JsonLexer.Lex("{\"key\":\"value\"}");

            Assert.IsNotNull(lex);

            string[] expected = new string[] { "{", "key", ":", "value", "}" };
            for (int i = 0; i < expected.Length; i++) Assert.That(lex[i] == expected[i]);
        }

        [Test]
        public void LexerTestSimpleList()
        {
            string[] lex = JsonLexer.Lex("[\"li1\",\"li2\"]");

            Assert.IsNotNull(lex);

            string[] expected = new string[] { "[", "li1", ",", "li2", "]" };
            for (int i = 0; i < expected.Length; i++) Assert.That(lex[i] == expected[i]);
        }

        [Test]
        public void LexerTestListOfObjects()
        {
            string[] lex = JsonLexer.Lex("[{\"key1\":\"val1\"},{\"key2\":\"val2\"}]");

            Assert.IsNotNull(lex);

            string[] expected = new string[] { "[", "{", "key1", ":", "val1", "}", ",", "{", "key2", ":", "val2", "}", "]" };
            for (int i = 0; i < expected.Length; i++) Assert.That(lex[i] == expected[i]);
        }

        [Test]
        public void LexerTestObjectWithListValue()
        {
            string[] lex = JsonLexer.Lex("{\"key1\":[\"li1\",\"li2\"]}");

            Assert.IsNotNull(lex);

            string[] expected = new string[] { "{", "key1", ":", "[", "li1", ",", "li2", "]", "}" };
            for (int i = 0; i < expected.Length; i++) Assert.That(lex[i] == expected[i]);
        }

        // A Test behaves as an ordinary method
        [Test]
        public void LexerTestOpenAiExample()
        {
            string[] lex = JsonLexer.Lex("{\"id\":    \"cmpl - uqkvlQyYK7bGYrRHQ0eXlWi7\",\"object\": \"text_completion\", \"created\":     1589478378,\"model\":    \"davinci:2020-05-03\",\"choices\": [{\"text\": \" there was a girl who\",\"index\":  0,\"logprobs\": null,\"finish_reason\":    \"length\"}]}  ");

            Assert.IsNotNull(lex);

            string[] expected = new string[] { "{", "id", ":", "cmpl - uqkvlQyYK7bGYrRHQ0eXlWi7", ",", "object", ":", "text_completion", ",", "created", ":", "1589478378", ",", "model", ":", "davinci:2020-05-03", ",", "choices", ":", "[", "{", "text", ":", " there was a girl who", ",", "index", ":", "0", ",", "logprobs", ":", "null", ",", "finish_reason", ":", "length", "}", "]", "}" };

            for (int i = 0; i < expected.Length; i++) Assert.That(lex[i] == expected[i]);
        }
    }
}