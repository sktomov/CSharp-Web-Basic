namespace WebServer.Server
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Survey
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDay { get; set; }

        public string Gender { get; set; }

        public string Status { get; set; }

        public string Recommendations { get; set; }

        public string Owns { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"FirstName: {FirstName}");
            sb.AppendLine($"LastName: {LastName}");
            sb.AppendLine($"BirthDay: {BirthDay.ToShortDateString()}");
            sb.AppendLine($"Gender: {Gender}");
            sb.AppendLine($"Status: {Status}");
            sb.AppendLine($"Recommendations: {Recommendations}");
            sb.AppendLine($"Owns: {Owns}");

            return sb.ToString().Trim();
        }
    }

}
