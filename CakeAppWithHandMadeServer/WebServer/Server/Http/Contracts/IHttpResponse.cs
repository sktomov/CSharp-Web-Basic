namespace WebServer.Server.Http.Contracts
{

    public interface IHttpResponse
    {
        string Response { get; }

        void AddHeader(string headerField, string url);
    }
}
