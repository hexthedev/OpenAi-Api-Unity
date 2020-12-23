using NUnit.Framework;

using System;
using System.Collections;
using System.IO;
using System.Security.Authentication;
using System.Text;

using UnityEngine;

namespace OpenAiApi
{
    public class OpenAiApiV1Tests
    {
        private const string cOpenAiKeyName = "OPENAI_PRIVATE_KEY";

        /// <summary>
        /// This will only work if you have a secret key as the environment variable OPENAI_PRIVATE_KEY
        /// </summary>
        [Test]
        public async void OpenAiApiV1Base()
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

            OpenAiApiV1 api = new OpenAiApiV1(key);

            CompletionResponseV1 res =  await api.Engines.Engine("davinci").Completions.CreateCompletionAsync(
                new CompletionRequestV1() { prompt = "hello", max_tokens = 8 }
            );

            Assert.IsNotNull(res);
        }
    }
}