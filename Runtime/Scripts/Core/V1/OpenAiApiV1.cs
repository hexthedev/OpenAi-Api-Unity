using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using UnityEngine;

namespace OpenAiApi
{
    public class OpenAiApiV1 : IResource
    {
        private string _authKey;

        public IResource ParentResource => null;

        public string Endpoint => "https://api.openai.com/v1";

        public string Url => Endpoint;

        public EnginesResource Engines { get; private set; }

        public OpenAiApiV1(string authKey)
        {
            _authKey = authKey;
            Engines = new EnginesResource(this);
        }

        public void ConstructEndpoint(StringBuilder sb)
        {
            sb.Append(Endpoint);
        }

        public void PopulateAuthHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authKey);
            //client.DefaultRequestHeaders.Add("User-Agent", "okgodoit/dotnet_openai_api");
        }

        public void PopulateAuthHeaders(HttpRequestMessage message)
        {
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authKey);
            //client.Headers.Add("User-Agent", "okgodoit/dotnet_openai_api");
        }
    }
}