using System;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// Images request
    /// </summary>
    public static partial class ExtensionMethods
    {
        public enum IMAGE_SIZE
        {
            imsize_256x256
                , imsize_512x512
                , imsize_1024x1024
        }
        public static string ToJSONString(this IMAGE_SIZE? imSize)
        {
            switch (imSize)
            {
                case IMAGE_SIZE.imsize_256x256:
                    return "256x256";
                case IMAGE_SIZE.imsize_512x512:
                    return "512x512";
                case IMAGE_SIZE.imsize_1024x1024:
                    return "1024x1024";
                default:
                    throw new ArgumentException($"Invalid enum value '{imSize}' for image size");
            }
        }
        public static IMAGE_SIZE ToImageSize(this string imSizeS)
        {
            switch (imSizeS)
            {
                case "256x256":
                    return IMAGE_SIZE.imsize_256x256;
                case "512x512":
                    return IMAGE_SIZE.imsize_512x512;
                case "1024x1024":
                    return IMAGE_SIZE.imsize_1024x1024;
                default:
                    throw new ArgumentException($"Invalid string value '{imSizeS}' for image size");
            }
        }
        public enum IMAGE_RESPONSE_FORMAT
        {
            url
                , b64_json
        }
        public static string ToJSONString(this IMAGE_RESPONSE_FORMAT? imResponseFormat)
        {
            switch (imResponseFormat)
            {
                case IMAGE_RESPONSE_FORMAT.url:
                    return "url";
                case IMAGE_RESPONSE_FORMAT.b64_json:
                    return "b64_json";
                default:
                    throw new ArgumentException($"Invalid enum value '{imResponseFormat}' for image response format");
            }
        }
        public static IMAGE_RESPONSE_FORMAT ToImageResponseFormat(this string imResponseFormatS)
        {
            switch (imResponseFormatS)
            {
                case "url":
                    return IMAGE_RESPONSE_FORMAT.url;
                case "b64_json":
                    return IMAGE_RESPONSE_FORMAT.b64_json;
                default:
                    throw new ArgumentException($"Invalid string value '{imResponseFormatS}' for image response format");
            }
        }
    }
}