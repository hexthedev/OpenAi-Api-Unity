namespace OpenAi.Json
{
    /// <summary>
    /// Can be converted to and from json
    /// </summary>
    public interface IJsonable
    {
        /// <summary>
        /// Convert the object to json format
        /// </summary>
        /// <returns>Object in json format string</returns>
        string ToJson();

        /// <summary>
        /// Update the objects values based on JsonObject
        /// </summary>
        /// <param name="json">JsonObject representing object instance</param>
        void FromJson(JsonObject json);
    }
}
