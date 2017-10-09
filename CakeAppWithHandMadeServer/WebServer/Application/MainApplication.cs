namespace WebServer.Application
{
    using Application.Controllers;
    using Server.Handlers;
    using Server.Http.Contracts;
    using Server.Routing.Contracts;
    using System;
    using System.Globalization;
    using System.Net;
    using System.Text;

    public class MainApplication : IApplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AddRoute("/", new GetRequestHandler(httpContext => new HomeController().Index()));
            appRouteConfig.AddRoute("/about", new GetRequestHandler(httpContext => new HomeController().AboutUs()));
            appRouteConfig.AddRoute("/register", new GetRequestHandler(httpContext => new UserController().RegisterGet()));
            appRouteConfig.AddRoute("/register",new PostRequestHandler(httpContext => new UserController().RegisterPost(httpContext.Request.FormData["name"])));
            appRouteConfig.AddRoute("/user/{(?<name>[a-z]+)}", new GetRequestHandler(httpContext => new UserController().Details(httpContext.Request.UrlParameters["name"])));
            appRouteConfig.AddRoute("/add", new GetRequestHandler(httpContext => new CakeController().AddGet()));
            appRouteConfig.AddRoute("/add", new PostRequestHandler(httpContext => new CakeController().AddPost(httpContext.Request.FormData["name"], decimal.Parse(httpContext.Request.FormData["price"]))));
            appRouteConfig.AddRoute("/search", new GetRequestHandler(httpContext => new CakeController().SearchGet()));
            appRouteConfig.AddRoute("/search", new PostRequestHandler(httpContext => new CakeController().SearchPost(httpContext.Request.FormData["searchString"])));
            appRouteConfig.AddRoute("/calculator", new GetRequestHandler(httpContext => new CalculatorController().CalculatorGet()));
            appRouteConfig.AddRoute("/calculator", new PostRequestHandler(httpContext => new CalculatorController()
            .CalculatorPost(int.Parse(httpContext.Request.FormData["firstNum"]), WebUtility.UrlDecode(httpContext.Request.FormData["sign"]), int.Parse(httpContext.Request.FormData["SecondNum"]))));

            appRouteConfig.AddRoute("/login", new GetRequestHandler(httpContext => new UserController().LoginGet()));
            appRouteConfig.AddRoute("/login", new PostRequestHandler(httpContext => new UserController()
            .LoginPost(httpContext.Request.FormData["username"], httpContext.Request.FormData["password"])));

            appRouteConfig.AddRoute("/send", new GetRequestHandler(httpContext => new UserController().SendGet()));
            appRouteConfig.AddRoute("/send", new PostRequestHandler(httpContext => new UserController().SendPost(httpContext.Request.FormData["subject"])));

            appRouteConfig.AddRoute("/survey", new GetRequestHandler(httpContext => new SurveyController().SurveyGet()));
            appRouteConfig.AddRoute("/survey", new PostRequestHandler(httpContext => new SurveyController()
            .SurveyPost(httpContext.Request.FormData["firstName"], httpContext.Request.FormData["lastName"],httpContext.Request.FormData["birthDate"], httpContext.Request.FormData["gender"], httpContext.Request.FormData["select"], httpContext.Request.FormData["recommendations"], httpContext.Request.FormData["owns"])));

        }

    }
}
