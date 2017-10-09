namespace WebServer.Application.Views
{
    using Server.Contracts;
    using System.IO;

    public class HomeIndexView : IView
    {
        public string View()
        {
            //:\SoftUniResources\C#Web\CSharp-Web-Basic\trunk\Resources\index.html'.))'
            string dirpath = Directory.GetCurrentDirectory();
            var path = dirpath + @"\Application\Resources\index.html";
            var result = File.ReadAllText(path);
            return result;
        }
    }
}
