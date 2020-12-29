using NUnit.Framework;

using OpenAi.Json;

using UnityEngine.TestTools;

namespace OpenAiApi
{
    public class JsonDeserializerTests
    {
        [Test]
        public void DeserializeTestBasic()
        {
            string json = "{\"key1\":\"val1\"}";
            JsonObject obj = JsonDeserializer.FromJson(json);

            obj.AssertRootIsObject();
            obj.NestedValues.AssertIsValidJsonObjectArray(1);

            obj.NestedValues[0].AssertValue("key1", "val1");
        }

        [Test]
        public void DeserializeTestComplexe()
        {
            string json = "{\"key1\":\"val1\", \"key2\":\"val2\", \"list1\":[\"li1\n\", 67, 1.234], \"objt\" : { \"obj1\":\"ob1\", \"obj2\":\"ob2\", \"obj3\": [1, 1] }}";
            JsonObject obj = JsonDeserializer.FromJson(json);

            Assert.IsNotNull(obj);

            //AssertValue(obj.NestedValue[0], "key1", "val1");
            //AssertValue(obj.NestedValue[1], "key2", "val2");
            //AssertList(obj.NestedValue[2], "list1", "li1\n", "67", "1.234");

            JsonObject objt = obj.NestedValues[3];
            Assert.That(objt.Type == EJsonType.Object);

            //AssertValue(objt.NestedValue[0], "obj1", "ob1");
            //AssertValue(objt.NestedValue[1], "obj2", "ob2");
            //AssertList(objt.NestedValue[2], "obj3", "1", "1");
        }
    }
}