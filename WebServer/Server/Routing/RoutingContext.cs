namespace WebServer.Server.Routing
{
    using Contracts;
    using Handlers;
    using System.Collections.Generic;

    public class RoutingContext : IRoutingContext
    {
        private readonly List<string> parameters;

        public RoutingContext(RequestHandler handler, List<string> parameters)
        {
            this.parameters = parameters;
            this.RequestHandler = handler;
        }

        public IEnumerable<string> Parameters => this.parameters;
        public RequestHandler RequestHandler { get; }
    }
}
