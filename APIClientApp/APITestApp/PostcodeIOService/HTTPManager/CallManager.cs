using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITestApp.PostcodeIOService.HTTPManager
{
    public class CallManager
    {
        //Restsharp object that handles comms with the API
        private readonly IRestClient _client;

        //capture status
        public int StatusCode { get; set; }

        public CallManager()
        {
            _client = new RestClient(AppConfigReader.BaseUrl);
        }

        public CallManager(string uri)
        {
            _client = new RestClient(uri);
        }

        ///<summary>
        ///define and makes API and stores the response
        ///</summary>
        ///<param name="postcode"></param>
        public async Task<string> MakePostcodeRequestAsync(string postcode)
        {
            //set up the request
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            //define the request resource path
            request.Resource = $"postcodes/{postcode.ToLower().Replace(" ", "")}";

            //Make request, (doesn't instantiate the interface. Just uses as type
            IRestResponse response = await _client.ExecuteAsync(request);
            StatusCode = (int)response.StatusCode;
            return response.Content;
        }

        public async Task<string> MakePostcodeRequestAsync(string[] postcodes)
        {
            //set up request
            var request = new RestRequest();
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");
            
            var postcodesJObject = new JObject
            {
                new JProperty("postcodes",
                new JArray(postcodes))
            };

            //define the request resource path
            request.AddParameter("application/json", postcodesJObject.ToString(), ParameterType.RequestBody);


            //Make request, (doesn't instantiate the interface. Just uses as type
            IRestResponse response = await _client.ExecuteAsync(request);

            //Parse JSON in response content
            StatusCode = (int)response.StatusCode;
            return response.Content;

        }
    }
}
