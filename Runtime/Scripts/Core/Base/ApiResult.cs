using System.Net.Http;

namespace OpenAiApi
{
    public class ApiResult<TResult>
    {
        public bool IsSuccess;

        public HttpResponseMessage HttpResponse;

        public TResult Result;
    }
}