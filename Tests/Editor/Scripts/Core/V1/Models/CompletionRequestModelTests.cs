using NUnit.Framework;

namespace OpenAi.Api.V1.Test
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
            string expected = "{\"echo\":true,\"stop\":[\"\\n\",\"a\"]}";

            Assert.That(json == expected);
        } 
    }
}
