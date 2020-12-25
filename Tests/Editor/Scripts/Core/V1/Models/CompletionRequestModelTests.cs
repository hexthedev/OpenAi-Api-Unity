using NUnit.Framework;

namespace OpenAiApi
{
    class CompletionRequestModelTests
    {
        [Test]
        public void ToJsonBase()
        {
            CompletionRequestV1 crm = new CompletionRequestV1()
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
