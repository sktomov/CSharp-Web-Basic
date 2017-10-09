namespace WebServer.Server.Http
{
    using Server.Contracts;
    using System.Net;

    public class ViewResponse : HttpResponse
    {
        public ViewResponse(HttpStatusCode responseCode, IView view) : base(responseCode, view)
        {
        }
    }
}
