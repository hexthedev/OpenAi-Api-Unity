using NUnit.Framework;

namespace OpenAiApi
{
    public class JsonLexerTests
    {
        [Test]
        public void LexerTestBase()
        {
            string[] lex = JsonLexer.Lex("{\"hello\"}");

            Assert.IsNotNull(lex);

            string[] expected = new string[] { "{", "hello", "}" };
            for (int i = 0; i < expected.Length; i++) Assert.That(lex[i] == expected[i]);
        }

        // A Test behaves as an ordinary method
        [Test]
        public void LexerTestComplex()
        {
            string[] lex = JsonLexer.Lex("{\"id\":    \"cmpl - uqkvlQyYK7bGYrRHQ0eXlWi7\",\"object\": \"text_completion\", \"created\":     1589478378,\"model\":    \"davinci:2020-05-03\",\"choices\": [{\"text\": \" there was a girl who\",\"index\":  0,\"logprobs\": null,\"finish_reason\":    \"length\"}]}  ");

            Assert.IsNotNull(lex);

            string[] expected = new string[] { "{", "id", ":", "cmpl - uqkvlQyYK7bGYrRHQ0eXlWi7", ",", "object", ":", "text_completion", ",", "created", ":", "1589478378", ",", "model", ":", "davinci:2020-05-03", ",", "choices", ":", "[", "{", "text", ":", " there was a girl who", ",", "index", ":", "0", ",", "logprobs", ":", "null", ",", "finish_reason", ":", "length", "}", "]", "}" };

            for (int i = 0; i < expected.Length; i++) Assert.That(lex[i] == expected[i]);
        }
    }
}