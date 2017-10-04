namespace WebServer.Server.Http
{
    using Contracts;
    using Enums;
    using Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Net;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.HeaderCollection = new HttpHeaderCollection();
            this.UrlParameters = new Dictionary<string, string>();
            this.QueryParameters = new Dictionary<string, string>();
            this.FormData = new Dictionary<string, string>();
            this.ParseRequest(requestString);
        }

        public Dictionary<string, string> FormData { get; private set; }

        public HttpHeaderCollection HeaderCollection { get; private set; }

        public string Path { get; private set; }

        public Dictionary<string, string> QueryParameters { get; private set; }

        public HttpRequestMethod RequestMethod { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, string> UrlParameters { get; private set; }

        public void AddUrlParameter(string key, string value)
        {
            this.UrlParameters.Add(key, value);
        }

        public void ParseRequest(string requestString)
        {
            var requestLines = requestString.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

            var requestLine = requestLines[0].Trim().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            if (requestLine.Length != 3 || requestLine[2].ToLower() != "http/1.1")
            {
                throw new BadRequestException("Invalid request line");
            }

            this.RequestMethod = this.ParseRequestMethod(requestLine[0]);

            if (this.RequestMethod == HttpRequestMethod.POST)
            {
                this.ParseQuery(requestLines[requestLines.Length - 1], this.FormData);
            }

            this.Url = requestLine[1];
            this.Path = this.Url.Split(new[] {'?', '#'}, StringSplitOptions.RemoveEmptyEntries)[0];
            this.ParseHeaders(requestLines);
            this.ParseParameters();
        }

        private void ParseParameters()
        {
            if (!this.Url.Contains("?"))
            {
                return;
            }
            string query = this.Url.Split('?')[1];
            this.ParseQuery(query, QueryParameters);
        }

        private void ParseQuery(string query, Dictionary<string, string> dict)
        {
            if (!query.Contains("="))
            {
                return;
            }
            var queryPairs = query.Split('&');
            foreach (var queryPair in queryPairs)
            {
                var queryArgs = queryPair.Split('=');
                if (queryArgs.Length != 2)
                {
                    continue;
                }

                dict.Add(WebUtility.UrlDecode(queryArgs[0]), WebUtility.UrlDecode(queryArgs[1]));
            }
        }

        private void ParseHeaders(string[] requestLines)
        {
            int endIndex = Array.IndexOf(requestLines, string.Empty);

            for (int i = 1; i < endIndex; i++)
            {
                var headersArgs = requestLines[i].Split(new[] {": "}, StringSplitOptions.None);
                var header = new HttpHeader(headersArgs[0], headersArgs[1]);
                this.HeaderCollection.Add(header);
            }

            if (!this.HeaderCollection.ContainsKey("Host"))
            {
                throw new BadRequestException("Invalid header");
            }
        }

        private HttpRequestMethod ParseRequestMethod(string requestMethod)
        {
            try
            {
                return Enum.Parse<HttpRequestMethod>(requestMethod, true);
            }
            catch (Exception e)
            {
               throw  new BadRequestException("Invalid request method");
            }
        }
    }
}
