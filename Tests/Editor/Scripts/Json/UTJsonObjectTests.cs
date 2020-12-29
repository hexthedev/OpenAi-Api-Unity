using NUnit.Framework;

using OpenAi.Json;

using System.Collections.Generic;

namespace OpenAiApi
{
    public static class UTJsonObjectTests
    {
        /// <summary>
        /// Asserts that a JsonObject is the root, and is an object type
        /// </summary>
        /// <param name="obj"></param>
        public static void AssertRootIsObject(this JsonObject obj)
        {
            Assert.IsNotNull(obj);
            Assert.That(obj.Name == null);
            Assert.That(obj.Type == EJsonType.Object);
        }

        /// <summary>
        /// Asserts that a JsonObject is the root, and is a list type
        /// </summary>
        /// <param name="obj"></param>
        public static void AssertRootIsList(this JsonObject obj)
        {
            Assert.IsNotNull(obj);
            Assert.That(obj.Name == null);
            Assert.That(obj.Type == EJsonType.List);
        }

        /// <summary>
        /// Asserts that a JsonObject is a value, and verifies the name and val
        /// of the JsonObject
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static void AssertValue(this JsonObject obj, string name, string val)
        {
            Assert.IsNotNull(obj);
            Assert.That(obj.Type == EJsonType.Value);
            Assert.That(obj.Name == name);
            Assert.That(obj.StringValue == val);
        }

        /// <summary>
        /// Asserts that the object is a list of a certain size, but does not check the element values
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static void AssertList(this JsonObject obj, int count)
        {
            Assert.IsNotNull(obj);
            Assert.That(obj.Type == EJsonType.List);
            obj.NestedValues.AssertIsValidJsonObjectArray(count);
        }

        /// <summary>
        /// Asserts that a JsonObject is a list of values, and verifies the name of the object
        /// and the value of each list member
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static void AssertListWithSimpleValues(this JsonObject obj, string name, params string[] vals)
        {
            Assert.IsNotNull(obj);
            Assert.That(obj.Type == EJsonType.List);
            Assert.That(obj.Name == name);

            for (int i = 0; i < obj.NestedValues.Count; i++)
            {
                Assert.IsNotNull(obj.NestedValues[i]);
                Assert.That(obj.NestedValues[i].Type == EJsonType.Value);
                Assert.That(vals[i] == obj.NestedValues[i].StringValue);
            }
        }

        /// <summary>
        /// Asserts that a JsonObject is an object that is in a list. This means the object has no name
        /// since it's a list value and has an index instead. 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static void AssertListElementIsObject(this JsonObject obj)
        {
            Assert.IsNotNull(obj);
            Assert.That(obj.Type == EJsonType.Object);
        }

        /// <summary>
        /// Asserts that a JsonObject array is not null and of a certain size
        /// </summary>
        /// <param name="obj"></param>
        public static void AssertIsValidJsonObjectArray(this List<JsonObject> objs, int count)
        {
            Assert.IsNotNull(objs);
            Assert.That(objs.Count == count);
        }
    }
}