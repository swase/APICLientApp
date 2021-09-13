using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace APITestApp.Services
{
    public class SinglePostcodeService
    {
        #region Properties
        //RestSharp Object that handles comms with the API client
        public RestClient Client;
        public JObject ResponseContent { get; set; }
        //The postcode used in this API request
        public string PostcodeSelected { get; set; }
        public int StatusCode { get; set; }

        public SinglePostcodeResponse ResponseObject { get; set; }


        #endregion

        public SinglePostcodeService()
        {
            Client = new RestClient { BaseUrl = new Uri(AppConfigReader.BaseUrl) };

        }

        public async Task MakeRequest(string postcode)
        {
            //set up request
            var request = new RestRequest();
            request.Method = Method.GET;
            request.AddHeader("Content-Type", "application/json");
            PostcodeSelected = postcode;

            //define the request resource path
            request.Resource = $"postcodes/{postcode.ToLower().Replace(" ", "")}";

            //Make request, (doesn't instantiate the interface. Just uses as type
            IRestResponse response = await Client.ExecuteAsync(request);

            //Parse JSON in response content
            ResponseContent = JObject.Parse(response.Content);

            StatusCode = (int)response.StatusCode;

            //Parse JSOn string into an object tree
            ResponseObject = JsonConvert.DeserializeObject<SinglePostcodeResponse>(response.Content);
        }
    }
}
