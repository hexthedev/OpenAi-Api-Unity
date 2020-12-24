namespace OpenAiApi
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
        public string ToJson();

        /// <summary>
        /// Update the objects values based on JsonObject
        /// </summary>
        /// <param name="json"></param>
        public void FromJson(JsonObject json);
    }
}
