using NUnit.Framework;

namespace OpenAiApi
{
    class CompletionRequestModelTests
    {
        [Test]
        public void ToJsonBase()
        {
            CompletionRequestModelV1 crm = new CompletionRequestModelV1()
            {
                stop = new StringOrArray("\n", "a"),
                echo = true
            };

            string json = crm.ToJson();
            string expected = "{\"echo\":True,\"stop\":[\"\n\",\"a\"]}";

            Assert.That(json == expected);
        } 
    }
}
