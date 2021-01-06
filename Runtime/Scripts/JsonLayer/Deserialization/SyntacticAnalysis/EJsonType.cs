namespace OpenAi.Json
{
    /// <summary>
    /// Types of objects in json
    /// </summary>
    public enum EJsonType
    {
        /// <summary>
        /// Simple value type. It may or may not have a key
        /// </summary>
        Value, 
        
        /// <summary>
        /// An object, or many "key":"values"
        /// </summary>
        Object, 

        /// <summary>
        /// A list of indexed values
        /// </summary>
        List
    }
}
