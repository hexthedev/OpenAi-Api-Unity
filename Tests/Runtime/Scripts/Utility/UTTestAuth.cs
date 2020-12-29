using OpenAi.Api.V1;
using OpenAi.Json;

using System;
using System.IO;
using System.Security.Authentication;
using System.Text;

namespace OpenAi.Api.Test
{
    public static class UTTestAuth
    {
        private const string cOpenAiKeyName = "OPENAI_PRIVATE_KEY";

        public static SAuthArgs GetAndValidateAuthKey()
        {
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string authPath = $"{userPath}/.openai/auth.json";
            FileInfo fi = new FileInfo(authPath);

            if (!fi.Exists) throw new AuthenticationException($"No authentication file exists at {authPath}");

            string json = null;
            using (FileStream fs = fi.OpenRead())
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
                json = Encoding.UTF8.GetString(buffer);
            }

            JsonObject des = JsonDeserializer.FromJson(json);
            
            SAuthArgs authArgs = new SAuthArgs();
            authArgs.FromJson(des);

            return authArgs;
        }
    }
}