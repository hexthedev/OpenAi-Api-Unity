using OpenAi.Json;
using static OpenAi.Api.V1.ExtensionMethods;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// Image generation request <see href="https://platform.openai.com/docs/api-reference/images/create"/>
    /// </summary>
    public class ImagesGenerationsRequestV1 : AModelV1
    {
        /// <summary>
        /// A text description of the desired image(s). The maximum length is 1000 characters.
        /// </summary>
        public string prompt;
        /// <summary>
        /// The number of images to generate. Must be between 1 and 10.
        /// </summary>
        public int? n;
        /// <summary>
        /// The size of the generated images. Must be one of 256x256, 512x512, or 1024x1024
        /// </summary>
        public IMAGE_SIZE? size;
        /// <summary>
        /// The format in which the generated images are returned. Must be one of url or b64_json
        /// </summary>
        public IMAGE_RESPONSE_FORMAT? response_format;
        /// <summary>
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse
        /// </summary>
        public string user;

        /// <inheritdoc />
        public override void FromJson(JsonObject json)
        {
            if (json.Type != EJsonType.Object) throw new OpenAiApiException("Deserialization failed, provided json is not an object");

            foreach (JsonObject obj in json.NestedValues)
            {
                switch (obj.Name)
                {
                    case nameof(this.prompt):
                        this.prompt = obj.StringValue;
                        break;
                    case nameof(this.n):
                        this.n = int.Parse(obj.StringValue);
                        break;
                    case nameof(this.size):
                        this.size = obj.StringValue.ToImageSize();
                        break;
                    case nameof(this.response_format):
                        this.response_format = obj.StringValue.ToImageResponseFormat();
                        break;
                    case nameof(user):
                        user = obj.StringValue;
                        break;
                }
            }
        }

        /// <inheritdoc />
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            jb.Add(nameof(this.prompt), this.prompt);
            jb.Add(nameof(this.n), this.n);
            jb.Add(nameof(this.size), this.size.ToJSONString());
            jb.Add(nameof(this.response_format), this.response_format.ToJSONString());
            jb.Add(nameof(user), user);
            jb.EndObject();

            return jb.ToString();
        }
    }
}
