namespace WebServer.Application.Views
{
    using Server.Contracts;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class AddView : IView
    {
        private readonly List<string> cakes;

        public AddView(List<string> cakes)
        {
            this.cakes = cakes;
        }

        public string View()
        {
            string dirpath = Directory.GetCurrentDirectory();
            var path = dirpath + @"\Application\Resources\add.html";
            var result = File.ReadAllText(path);
            result = result.Replace("<!--replace-->", "<pre>" + string.Join(Environment.NewLine, this.cakes) +"</pre>");
            return result;
        }

    }
}
