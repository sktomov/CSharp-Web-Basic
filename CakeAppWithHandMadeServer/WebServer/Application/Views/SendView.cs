namespace WebServer.Application.Views
{
    using Server.Contracts;
    using System.IO;

    public class SendView : IView
    {
        private string error;

        public SendView(string error)
        {
            this.error = error;
        }

        public string View()
        {
            string dirpath = Directory.GetCurrentDirectory();
            var path = dirpath + @"\Application\Resources\send.html";
            var result = File.ReadAllText(path);
            result = result.Replace("<!--replace-->", error);
            return result;
        }
    }
}
