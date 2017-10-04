namespace WebServer.Server
{
    using System;
    using System.Net;
    using Contracts;
    using Routing;
    using Routing.Contracts;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    public class WebServer : IRunnable
    {
        private readonly int port;
        private readonly IServerRouteConfig serverRouteConfig;
        private readonly TcpListener listener;
        private bool isRuning;

        public WebServer(int port, IAppRouteConfig routeConfig)
        {
            this.port = port;
            this.listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            this.serverRouteConfig = new ServerRouteConfig(routeConfig);
        }

        public void Run()
        {
            this.listener.Start();
            this.isRuning = true;
            Console.WriteLine($"Server started. Listening to TCP clients at 127.0.0.1:{port}");

            Task task = Task.Run(this.ListenLoop);
            task.Wait();
        }

        private async Task ListenLoop()
        {
            while (this.isRuning)
            {
                var client = await this.listener.AcceptSocketAsync();
                var connectionHandler = new ConnectionHandler(client, this.serverRouteConfig);
                var connection = connectionHandler.ProcessRequestAsync();
                connection.Wait();
            }
        }
    }
}
