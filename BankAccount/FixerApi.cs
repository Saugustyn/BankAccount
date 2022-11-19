using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace BankAccount
{
    internal class FixerApi : IFixerApi
    {
        private const string URI = "https://api.apilayer.com/fixer/";
        private string _apiKey = "kHactuL60GeLkGU9WbpG37ZGR3Qx1n4n";

        public JsonNode Convert(string amount, string from, string to)
        {
            var client = new RestClient($"{URI}convert?to={to}&from={from}&amount={amount}");
            var request = new RestRequest();
            request.AddHeader("apikey", _apiKey);
            var response = client.Execute(request);
            var json = JsonObject.Parse(response.Content);

            return json["result"];
        }

        public void Latest(string currance)
        {
            var client = new RestClient($"{URI}latest?symbols=&base={currance}");
            var request = new RestRequest();
            request.AddHeader("apikey", _apiKey);
            var response = client.Execute(request);

            Console.WriteLine(response.Content);
        }


    }
}
