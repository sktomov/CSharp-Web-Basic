namespace WebServer.Application.Views
{
    using System.IO;
    using Server.Contracts;

    public class AboutUsView : IView
    {
        public string View()
        {
            string dirpath = Directory.GetCurrentDirectory();
            var path = dirpath + @"\Application\Resources\aboutus.html";
            var result = File.ReadAllText(path);
            return result;
        }
    }
}
