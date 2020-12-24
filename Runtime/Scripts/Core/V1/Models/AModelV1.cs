namespace OpenAiApi
{
    /// <summary>
    /// A model that can be converted from json and populated from json
    /// </summary>
    public abstract class AModelV1 : IJsonable
    {
        /// <inheritdoc />
        public abstract void FromJson(JsonObject json);

        /// <inheritdoc />
        public abstract string ToJson();

        public T[] ArrayFromJson<T>(JsonObject parent) where T : AModelV1, new()
        {
            T[] newArray = new T[parent.NestedValues.Count];

            for(int i = 0; i<parent.NestedValues.Count; i++)
            {
                T model = new T();
                newArray[i] = model;
                model.FromJson(parent.NestedValues[i]);
            }

            return newArray;
        }
    }
}
