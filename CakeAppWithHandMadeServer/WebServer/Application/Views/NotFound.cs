namespace WebServer.Application.Views
{
    using WebServer.Server.Contracts;

    public class NotFound : IView
    {
        public string View()
        {
            return $"<body><h1>Not Found</h1></body>";
        }
    }
}
