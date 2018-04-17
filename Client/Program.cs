using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            GetThings();

            Console.ReadLine();
        }

        static async void GetThings()
        {
            var endpoint = await DiscoveryClient.GetAsync("http://localhost:60054/");

            if (endpoint.IsError == true)
            {
                Console.WriteLine(endpoint.Error);
                return;
            }

            var token = new TokenClient(endpoint.TokenEndpoint, "client", "secret");
            var response = await token.RequestClientCredentialsAsync("api1");

            if (response.IsError == true)
            {
                Console.WriteLine(response.Error);
                return;
            }

            Console.WriteLine(response.Json);

            var client = new HttpClient();
            client.SetBearerToken(response.AccessToken);

            var apiresponse = await client.GetAsync("http://localhost:60483/things");

            if (!apiresponse.IsSuccessStatusCode)
            {
                Console.WriteLine(apiresponse.StatusCode);
            }
            else
            {
                var content = await apiresponse.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
