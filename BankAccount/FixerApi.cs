using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Globalization;

namespace BankAccount
{
    internal class FixerApi : IFixerApi
    {
        private const string URI = "https://api.apilayer.com/fixer/";
        private string _apiKey = "kHactuL60GeLkGU9WbpG37ZGR3Qx1n4n";

        public decimal Convert(decimal amount, string from, string to)
        {
            decimal result;
            var client = new RestClient($"{URI}convert?to={to}&from={from}&amount={amount}");
            var request = new RestRequest();
            request.AddHeader("apikey", _apiKey);
            var response = client.Execute(request);
            var json = JsonObject.Parse(response.Content);
            string valueString = json["result"].ToString();

            result = decimal.Parse(valueString, CultureInfo.InvariantCulture);
            return result;

        }

        public void Latest(decimal currance)
        {
            var client = new RestClient($"{URI}latest?symbols=&base={currance}");
            var request = new RestRequest();
            request.AddHeader("apikey", _apiKey);
            var response = client.Execute(request);

            Console.WriteLine(response.Content);
        }


    }
}
