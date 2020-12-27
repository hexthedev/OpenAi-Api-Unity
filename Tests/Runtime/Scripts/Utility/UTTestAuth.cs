using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Authentication;
using System.Text;

namespace OpenAiApi
{
    public static class UTTestAuth
    {
        private const string cOpenAiKeyName = "OPENAI_PRIVATE_KEY";

        public static string GetAndValidateAuthKey()
        {
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string authPath = $"{userPath}/.openai/key.txt";
            FileInfo fi = new FileInfo(authPath);

            if (!fi.Exists) throw new AuthenticationException($"No authentication file exists at {authPath}");

            string key = null;
            using (FileStream fs = fi.OpenRead())
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
                key = Encoding.UTF8.GetString(buffer);
            }

            return key;
        }
    }
}