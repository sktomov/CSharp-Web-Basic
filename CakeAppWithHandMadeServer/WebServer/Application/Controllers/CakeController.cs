namespace WebServer.Application.Controllers
{
    using Server.Http;
    using System.Collections.Generic;
    using System.Net;
    using WebServer.Application.Views;
    using WebServer.Server.Http.Contracts;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class CakeController
    {
        private const string dbPath = @".\Application\Resources\Db\database.csv";
        private List<string> cakes;

        public CakeController()
        {
            this.cakes = new List<string>();
            this.LoadDb();
        }

        public IHttpResponse AddGet()
        {
            return new ViewResponse(HttpStatusCode.OK, new AddView(this.cakes));
        }

        public IHttpResponse AddPost(string name, decimal price)
        {
            File.AppendAllText(dbPath, $"Name: {name} Price: {price}{Environment.NewLine}");
            return new RedirectResponse($"/add");
        }

        public IHttpResponse SearchGet()
        {
            return new ViewResponse(HttpStatusCode.OK, new SearchView(null));
        }

        public IHttpResponse SearchPost(string searchString)
        {
            var sb = new StringBuilder();

            var result = this.cakes.Where(s => s.Contains(searchString)).ToList();
            foreach (var item in result)
            {
                sb.AppendLine(item);
            }

            return new ViewResponse(HttpStatusCode.OK, new SearchView($"<pre>{sb.ToString()}</pre>"));
        }

        private void LoadDb()
        {
            if (!File.Exists(dbPath))
            {
                File.Create(dbPath);
            }

            this.cakes = File.ReadAllLines(dbPath).ToList();
        }
    }
}
