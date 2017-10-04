namespace WebServer.Server.Handlers
{
    using System;
    using Contracts;
    using Http.Contracts;

    public abstract class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> func;

        protected RequestHandler(Func<IHttpRequest, IHttpResponse> func)
        {
            this.func = func;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            IHttpResponse httpResponse = this.func.Invoke(httpContext.Request);
            httpResponse.AddHeader("Content-Type", "text-html");

            return httpResponse;
        }
    }
}
