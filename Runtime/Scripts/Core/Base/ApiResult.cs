using System.Net.Http;

namespace OpenAi.Api.V1
{
    public class ApiResult<TResult>
    {
        public bool IsSuccess;

        public HttpResponseMessage HttpResponse;

        public TResult Result;
    }
}