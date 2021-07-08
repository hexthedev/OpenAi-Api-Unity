using OpenAi.Json;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// <see cref="https://beta.openai.com/docs/api-reference/classifications/create#classifications/create-examples"/>
    /// </summary>
    public class SelectedDocumentV1 : AModelV1
    {
        /// <summary>
        /// The document the label is associated to
        /// </summary>
        public string document;

        /// <summary>
        /// The text assoicated with the document
        /// </summary>
        public string text;

        /// <inheritdoc/>
        public override void FromJson(JsonObject json)
        {
            foreach (JsonObject jo in json.NestedValues)
            {
                switch (jo.Name)
                {
                    case nameof(document):
                        document = jo.StringValue;
                        break;
                    case nameof(text):
                        text = jo.StringValue;
                        break;
                }
            }
        }

        /// <inheritdoc/>
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            jb.Add(nameof(document), document);
            jb.Add(nameof(text), text);
            jb.EndObject();

            return jb.ToString();
        }
    }
}