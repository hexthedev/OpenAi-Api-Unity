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
        /// <returns></returns>
        string ToJson();

        /// <summary>
        /// Update the objects values based on JsonObject
        /// </summary>
        /// <param name="json"></param>
        void FromJson(JsonObject json);
    }
}
