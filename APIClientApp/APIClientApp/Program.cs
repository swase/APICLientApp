using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Linq;

namespace APIClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ///// SET UP REQUEST/////
            // Client Property which is equal to a new 'RestSharp'.
            // We are going to create a URI objects which encapsulates
            var restClient = new RestClient(@"https://api.postcodes.io/");

            // Set up the request
            var restRequest = new RestRequest(Method.GET); // default parameter is method.get(dont need to put in parameters if this is the case)
            // Set method as GET
            restRequest.Method = Method.GET; // optional
            // Added Header info
            restRequest.AddHeader("Content-Type", "application/json");
            // Set timeout
            restRequest.Timeout = -1;
            var postcode = "EC2Y 5AS";
            // Define request resource path
            restRequest.Resource = $"postcodes/{postcode.ToLower().Replace(" ", "")}";


            ///// EXECUTE REQUEST /////
            var singlePostcodeResponse = restClient.Execute(restRequest);

            //Console.WriteLine("Response Content as string");
            //Console.WriteLine(singlePostcodeResponse.Content);

            ///// SETUP BULKPOSTCODE REQUEST/////
            var client = new RestClient("https://api.postcodes.io/postcodes");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            JObject postcodes = new JObject
            {
                new JProperty("postcodes", new JArray(new string[]{"OX49 5NU", "M32 0JG", "NE30 1DP" }))
            };
            request.AddParameter("application/json", postcodes.ToString(), ParameterType.RequestBody);
            IRestResponse bulkPostcodeResponse = client.Execute(request);
            //Console.WriteLine(bulkPostcodeResponse.Content);

            ///Query our response as a JObject ///////
            ///
            var bulkJsonResponse = JObject.Parse(bulkPostcodeResponse.Content);
            var singleJsonResponse = JObject.Parse(singlePostcodeResponse.Content);
            //Console.WriteLine(singleJsonResponse["status"]);
            //Console.WriteLine(singleJsonResponse["result"]["country"]);
            //Console.WriteLine(singleJsonResponse["result"]["parish"]);
            //Console.WriteLine(bulkJsonResponse["result"][1]["result"]["country"]);

            var singPostCode = JsonConvert.DeserializeObject<SinglePostCodeResponse>(singlePostcodeResponse.Content);

            var bulkPostCode = JsonConvert.DeserializeObject<BulkPostCodeResponse>(bulkPostcodeResponse.Content);

            Console.WriteLine(singPostCode.result.country);

            foreach (var result in bulkPostCode.result)
            {
                Console.WriteLine(result.query);
                Console.WriteLine(result.postCode.region);
            }
            var result2 = bulkPostCode.result.Where(p => p.query == "OX49 5NU").Select(p => p.postCode.parish).FirstOrDefault();


        }
    }
}
