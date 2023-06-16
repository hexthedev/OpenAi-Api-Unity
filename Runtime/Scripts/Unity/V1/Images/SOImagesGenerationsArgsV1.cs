using OpenAi.Api.V1;
using UnityEngine;
using static OpenAi.Api.V1.ExtensionMethods;

namespace OpenAi.Unity.V1
{
    [CreateAssetMenu(fileName = "ImagesArgs", menuName = "OpenAi/Unity/V1/ImagesArgs")]
    public class SOImagesGenerationsArgsV1 : ScriptableObject
    {
        [Tooltip("The number of images to generate. Must be between 1 and 10.")]
        [Range(1, 10)]
        public int n = 2;

        [Tooltip("The size of the generated images. Must be one of 256x256, 512x512, or 1024x1024")]
        public IMAGE_SIZE imageSize = IMAGE_SIZE.imsize_512x512;

        [Tooltip("The format in which the generated images are returned. Must be one of url or b64_json")]
        public IMAGE_RESPONSE_FORMAT imageFormat = IMAGE_RESPONSE_FORMAT.url;

        [Tooltip("A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.")]
        public string user = "";

        public ImagesGenerationsRequestV1 AsImagesGenerationsRequest()
        {
            return new ImagesGenerationsRequestV1()
            {
                n = this.n,
                size = this.imageSize,
                response_format = this.imageFormat,
                user = this.user
            };
        }
    }
}