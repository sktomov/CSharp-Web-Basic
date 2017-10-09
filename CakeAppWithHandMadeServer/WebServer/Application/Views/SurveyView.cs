namespace WebServer.Application.Views
{
    using Server.Contracts;
    using System.IO;

    public class SurveyView : IView
    {
        private string error;

        public SurveyView(string error)
        {
            this.error = error;
        }

        public string View()
        {
            string dirpath = Directory.GetCurrentDirectory();
            var path = dirpath + @"\Application\Resources\survey.html";
            var result = File.ReadAllText(path);
            result = result.Replace("<!--replace-->", this.error);
            return result;
        }
    }
}
