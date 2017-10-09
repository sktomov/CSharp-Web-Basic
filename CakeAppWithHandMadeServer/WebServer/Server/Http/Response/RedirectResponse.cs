namespace WebServer.Server.Http
{
    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string redirectUrl) : base(redirectUrl)
        {
        }
    }
}
