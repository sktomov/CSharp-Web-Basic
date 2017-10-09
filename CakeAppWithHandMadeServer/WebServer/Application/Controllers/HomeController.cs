namespace WebServer.Application.Controllers
{
    using Server.Http;
    using Server.Http.Contracts;
    using System.Net;
    using Views;

    public class HomeController
    {
        public IHttpResponse Index()
        {
            return new ViewResponse(HttpStatusCode.OK, new HomeIndexView());
        }

        public IHttpResponse AboutUs()
        {
            return new ViewResponse(HttpStatusCode.OK, new AboutUsView());
        }
    }
}
