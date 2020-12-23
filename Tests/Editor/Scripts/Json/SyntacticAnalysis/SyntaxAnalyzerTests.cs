using NUnit.Framework;

namespace OpenAiApi
{
    class SyntaxAnalyzerTests
    {
        [Test]
        public void AnalyzeTestBase()
        {
            string[] syntax = new string[]
            {
                "{", "test", ":", "value", "}"
            };

            JsonObject obj = SyntaxAnalyzer.Parse(syntax);

            Assert.That(obj.Type == EJsonType.Object);

            Assert.That(obj.NestedValue[0].Name == "test");
            Assert.That(obj.NestedValue[0].StringValue == "value");
            Assert.That(obj.NestedValue[0].Type == EJsonType.Value);
        }

        [Test]
        public void AnalyzeTestComplexe()
        {
            string[] syntax = new string[]
            {
                "{", 
                "test", ":",
                   "[", "value1", ",", "value2", "]", ",", 
                "another", ":", "value3", ",", 
                "a1", ":", "b1", ",", 
                "obj", ":", 
                   "{", 
                   "field1", ":", "obj1", ",", 
                   "li", ":", 
                      "[", "val", ",", "val1", "]", 
                   "}", 
                "}"
            };

            JsonObject obj = SyntaxAnalyzer.Parse(syntax);
        }
    }
}
