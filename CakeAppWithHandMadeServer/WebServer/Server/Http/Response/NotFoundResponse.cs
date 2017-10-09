namespace WebServer.Server.Http.Response
{
    using System.Net;
    using Server.Contracts;

    public class NotFoundResponse : HttpResponse
    {
        public NotFoundResponse(HttpStatusCode responseCode, IView view) : base(responseCode, view)
        {
        }
    }
}
