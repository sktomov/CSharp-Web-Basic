namespace WebServer.Server.Handlers
{
    using System;
    using Http.Contracts;

    public class GetRequestHandler : RequestHandler
    {
        public GetRequestHandler(Func<IHttpContext, IHttpResponse> func) : base(func)
        {
        }
    }
}
