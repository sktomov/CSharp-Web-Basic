namespace WebServer.Application.Controllers
{
    using Application.Views;
    using Server.Http;
    using Server.Http.Contracts;
    using System.Linq;
    using System.Net;

    public class CalculatorController
    {
        private char[] signs = new char[] { '+', '-', '*', '/' };

        public IHttpResponse CalculatorGet()
        {
            return new ViewResponse(HttpStatusCode.OK, new CalculatorView(null));
        }

        public IHttpResponse CalculatorPost(int firstNum, string sign, int secondNum)
        {
            string replace = string.Empty;
            bool isValid = true;
            if (sign.Length > 1 || string.IsNullOrEmpty(sign) || !this.signs.Contains(sign[0]))
            {
                replace = "Invalid Sign.";
                isValid = false;
            }

            if (isValid)
            {
                int result = 0;
                
                switch (sign[0])
                {
                    case '*':
                        result = firstNum * secondNum;
                        break;
                    case '-':
                        result = firstNum - secondNum;
                        break;
                    case '+':
                        result = firstNum + secondNum;
                        break;
                    case '/':
                        result = firstNum / secondNum;
                        break;
                    default:
                        break;
                }

                replace = $"Result: {result}";
            }
            
            

            return new ViewResponse(HttpStatusCode.OK, new CalculatorView(replace));
        }

    }
}
