namespace WebServer.Application.Controllers
{
    using Application.Views;
    using Server.Http;
    using Server.Http.Contracts;
    using System.Net;
    using Server;

    public class UserController
    {
        public IHttpResponse RegisterGet()
        {
            return new ViewResponse(HttpStatusCode.OK, new RegisterView());
        }

        public IHttpResponse RegisterPost(string name)
        {
            return new RedirectResponse($"/user/{name}");
        }

        public IHttpResponse Details(string name)
        {
            var model = new Model { ["name"] = name };
            return new ViewResponse(HttpStatusCode.OK, new UserDetailsView(model));
        }

        public IHttpResponse LoginGet()
        {
            return new ViewResponse(HttpStatusCode.OK, new LoginView(null));
        }

        public IHttpResponse LoginPost(string userName, string password)
        {
            //string result = $"Hi {userName}, your password is {password}";
            if(userName != "suAdmin" || password != "aDmInPw17")
            {
                return new ViewResponse(HttpStatusCode.OK, new LoginView("Invalid username or password!"));
            }
            else
            {
                return new RedirectResponse("/send");
            }
            
        }

        public IHttpResponse SendGet()
        {
            return new ViewResponse(HttpStatusCode.OK, new SendView(null));
        }

        public IHttpResponse SendPost(string subject)
        {
            if(subject.Length > 100)
            {
                return new ViewResponse(HttpStatusCode.OK, new SendView("Subject must be less than 100 symbols."));
            }
            else
            {
                return new RedirectResponse("/");
            }
            
        }

    }
}
