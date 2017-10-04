namespace WebServer.Server.Routing
{
    using Contracts;
    using Enums;
    using Handlers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class ServerRouteConfig : IServerRouteConfig
    {
        public ServerRouteConfig(IAppRouteConfig appRouteConfig)
        {
            this.Routes = new Dictionary<HttpRequestMethod, Dictionary<string, IRoutingContext>>();

            foreach (HttpRequestMethod httpRequestMethod in Enum.GetValues(typeof(HttpRequestMethod)).Cast<HttpRequestMethod>())
            {
                this.Routes.Add(httpRequestMethod, new Dictionary<string, IRoutingContext>());
            }

            this.InitializeServerConfig(appRouteConfig);
        }

        public Dictionary<HttpRequestMethod, Dictionary<string, IRoutingContext>> Routes { get; }

        private void InitializeServerConfig(IAppRouteConfig appRouteConfig)
        {
            foreach (KeyValuePair<HttpRequestMethod, Dictionary<string, RequestHandler>> kvp in appRouteConfig.Routes)
            {
                foreach (KeyValuePair<string, RequestHandler> requestHandler in kvp.Value)
                {
                    var args = new List<string>();
                    var parseRegex = this.ParseRoute(requestHandler.Key, args);
                    var routingContext = new RoutingContext(requestHandler.Value, args);
                    this.Routes[kvp.Key].Add(parseRegex, routingContext);
                }
            }
        }

        private string ParseRoute(string requestHandlerKey, List<string> args)
        {
            var parsedRegex = new StringBuilder();
            parsedRegex.Append("^");
            if (requestHandlerKey == "/")
            {
                parsedRegex.Append($"{requestHandlerKey}$");
                return parsedRegex.ToString();
            }

            var tokens = requestHandlerKey.Split('/');
            this.ParseTokens(args, tokens, parsedRegex);

            return parsedRegex.ToString();
        }

        private void ParseTokens(List<string> args, string[] tokens, StringBuilder parsedRegex)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                var end = i == tokens.Length - 1 ? "$" : "/";
                if (!tokens[i].StartsWith("{") && !tokens[i].EndsWith("}"))
                {
                    parsedRegex.Append($"{tokens[i]}{end}");
                    continue;
                }

                var pattern = "<\\w+>";
                var regex = new Regex(pattern);
                Match match = regex.Match(tokens[i]);

                if (!match.Success)
                {
                    continue;
                }

                var paramName = match.Groups[0].Value.Substring(1, match.Groups[0].Length - 2);
                args.Add(paramName);
                parsedRegex.Append($"{tokens[i].Substring(1, tokens[i].Length - 2)}{end}");
            }
        }
    }
}
