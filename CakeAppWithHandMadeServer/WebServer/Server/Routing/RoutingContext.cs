namespace WebServer.Server.Routing
{
    using Contracts;
    using Handlers;
    using System.Collections.Generic;

    public class RoutingContext : IRoutingContext
    {

        public RoutingContext(RequestHandler handler, IEnumerable<string> parameters)
        {
            this.Parameters = parameters;
            this.RequestHandler = handler;
        }

        public IEnumerable<string> Parameters { get; private set; }

        public RequestHandler RequestHandler { get; }
    }
}
