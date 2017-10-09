namespace WebServer.Server.Handlers
{
    using System;
    using Contracts;
    using Http.Contracts;

    public abstract class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpContext, IHttpResponse> func;

        protected RequestHandler(Func<IHttpContext, IHttpResponse> func)
        {
            this.func = func;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            IHttpResponse httpResponse = this.func.Invoke(httpContext);
            httpResponse.AddHeader("Content-Type", "text-html");

            return httpResponse;
        }
    }
}
