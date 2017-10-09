namespace WebServer.Application.Views
{
    using System.IO;
    using WebServer.Server.Contracts;

    public class CalculatorView : IView
    {
        private string replace;

        public CalculatorView(string replace)
        {
            this.replace = replace;
        }

        public string View()
        {
            var path = @".\Application\Resources\calculator.html";
            var result = File.ReadAllText(path);
            result = result.Replace("<!--replace-->", this.replace);

            return result;
        }
    }
}
