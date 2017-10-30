using System;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TwillioTest
{
    public class Message
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public string Status { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("https://api.twilio.com/2010-04-01");

            var request = new RestRequest("Accounts/AC62b121f58c26107f78d4102b97ca218c/Messages.json", Method.GET);

			client.Authenticator = new HttpBasicAuthenticator("AC62b121f58c26107f78d4102b97ca218c", "c04b11526dc5b96bcec7fc5e46ad0251");

            var response = new RestResponse();

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();

            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
			Console.WriteLine(jsonResponse["messages"]);			
            Console.ReadLine();
        }
		public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
		{
			var tcs = new TaskCompletionSource<IRestResponse>();
			theClient.ExecuteAsync(theRequest, response => {
				tcs.SetResult(response);
			});
			return tcs.Task;
		}
    }
}
