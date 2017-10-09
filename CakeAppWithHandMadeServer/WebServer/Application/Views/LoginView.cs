namespace WebServer.Application.Views
{
    using System.IO;
    using WebServer.Server.Contracts;

    public class LoginView : IView
    {
        private string userInfo;

        public LoginView(string userInfo)
        {
            this.userInfo = userInfo;
        }

        public string View()
        {
            string dirpath = Directory.GetCurrentDirectory();
            var path = dirpath + @"\Application\Resources\login.html";
            var result = File.ReadAllText(path);
            result = result.Replace("<!--replace-->", this.userInfo);
            return result;
        }
    }
}
