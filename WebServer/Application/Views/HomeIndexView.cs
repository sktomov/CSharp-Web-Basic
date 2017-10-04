namespace WebServer.Application.Views
{
    using Server.Contracts;

    public class HomeIndexView : IView
    {
        public string View()
        {
            return "<body><h1>Welcome from server!</h1></body>";
        }
    }
}
