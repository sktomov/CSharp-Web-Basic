namespace WebServer.Application.Views
{
    using System.IO;
    using WebServer.Server.Contracts;

    public class SearchView : IView
    {
        private string searchResult;

        public SearchView(string searchResult)
        {
            this.searchResult = searchResult;
        }

        public string View()
        {
            var path = @".\Application\Resources\search.html";
            var result = File.ReadAllText(path);
            if (!string.IsNullOrEmpty(this.searchResult))
            {
                result = result.Replace("<!--replace-->", this.searchResult);
            }

            return result;
        }
    }
}
