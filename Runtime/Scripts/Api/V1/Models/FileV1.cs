using OpenAi.Json;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// Info about a file. <see cref="https://beta.openai.com/docs/api-reference/files/list"/>
    /// </summary>
    public class FileV1 : AModelV1
    {
        /// <summary>
        /// Engine id
        /// </summary>
        public string id;

        /// <summary>
        /// object type ("engine")
        /// </summary>
        public string obj;

        /// <summary>
        /// the byte size of the file
        /// </summary>
        public int? bytes;

        /// <summary>
        /// The unix epoch the file was created at
        /// </summary>
        public int? created_at;

        /// <summary>
        /// The name of the file
        /// </summary>
        public string filename;

        /// <summary>
        /// The use case the the file is ued for
        /// </summary>
        public string purpose;

        /// <inheritdoc />
        public override void FromJson(JsonObject json)
        {
            foreach(JsonObject jo in json.NestedValues)
            {
                switch (jo.Name)
                {
                    case nameof(id):
                        id = jo.StringValue;
                        break;
                    case "object":
                        obj = jo.StringValue;
                        break;
                    case nameof(bytes):
                        bytes = int.Parse(jo.StringValue);
                        break;
                    case nameof(created_at):
                        created_at = int.Parse(jo.StringValue);
                        break;
                    case nameof(filename):
                        filename = jo.StringValue;
                        break;
                    case nameof(purpose):
                        purpose = jo.StringValue;
                        break;

                }
            }
        }

        /// <inheritdoc />
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            jb.Add(nameof(id), id);
            jb.Add("object", obj);
            jb.Add(nameof(bytes), bytes);
            jb.Add(nameof(created_at), created_at);
            jb.Add(nameof(filename), filename);
            jb.Add(nameof(purpose), purpose);
            jb.EndObject();

            return jb.ToString();
        }
    }
}
