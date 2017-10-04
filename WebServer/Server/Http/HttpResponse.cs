using System.Text;

namespace WebServer.Server.Http
{
    using Server.Contracts;
    using Contracts;
    using System.Net;

    public abstract class HttpResponse : IHttpResponse
    {
        private readonly IView view;

        protected HttpResponse(string redirectUrl)
        {
            this.HeaderCollection = new HttpHeaderCollection();
            this.StatusCode = HttpStatusCode.Found;
            this.AddHeader("Location", redirectUrl);
        }

        protected HttpResponse(HttpStatusCode responseCode, IView view)
        {
            this.HeaderCollection = new HttpHeaderCollection();
            this.view = view;
            this.StatusCode = responseCode;
        }

        private HttpHeaderCollection HeaderCollection { get; set; }

        private HttpStatusCode StatusCode { get; set; }

        private string StatusMessage => this.StatusCode.ToString();

        public string Response
        {
            get
            {
                var response = new StringBuilder();
                response.AppendLine($"HTTP/1.1 {(int) this.StatusCode} {this.StatusMessage}");
                response.AppendLine(this.HeaderCollection.ToString());
                response.AppendLine();

                if ((int) this.StatusCode < 300 || (int) this.StatusCode > 400)
                {
                    response.AppendLine(this.view.View());
                }
                return response.ToString();
            }
        }

        public void AddHeader(string headerField, string url)
        {
            this.HeaderCollection.Add(new HttpHeader(headerField, url));
        }
    }
}
