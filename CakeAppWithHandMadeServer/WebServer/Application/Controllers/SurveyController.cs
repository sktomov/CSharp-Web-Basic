namespace WebServer.Application.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using WebServer.Application.Views;
    using WebServer.Server;
    using WebServer.Server.Http;
    using WebServer.Server.Http.Contracts;

    public class SurveyController
    {
        private const string ServeyPath = @".\Application\Resources\Db\survey.csv";

        public SurveyController()
        {
            this.CreateSurveyDb();
        }

        public IHttpResponse SurveyGet()
        {
            return new ViewResponse(HttpStatusCode.OK, new SurveyView(null));
        }

        public IHttpResponse SurveyPost(params string[] args)
        {
            //httpContext.Request.FormData["birthDate"], "dd/MM/yyyy", null)
            //string firstName, string lastName, DateTime birthDate, string gender, string status, string recommendations, string owns
            if(args.Any(a => string.IsNullOrEmpty(a)))
            {
                return new ViewResponse(HttpStatusCode.OK, new SurveyView("<p style=\"color:red\">All fields are required!</p>"));
            }
            else
            {
                var firstName = args[0];
                var lastName = args[1];
                var birthDate = DateTime.ParseExact(args[2], "dd/MM/yyyy", null);
                var gender = args[3];
                var status = args[4];
                var recommendations = args[5];
                var owns = args[6];
                var survey = new Survey
                {
                    FirstName = firstName,
                    LastName = lastName,
                    BirthDay = birthDate,
                    Gender = gender,
                    Owns = owns,
                    Recommendations = recommendations,
                    Status = status
                };

                
                File.AppendAllText(ServeyPath, survey.ToString());
            }
            return new RedirectResponse("/");
        }


        private void CreateSurveyDb()
        {
            if (!File.Exists(ServeyPath))
            {
                File.Create(ServeyPath);
            }

        }
    }
}
