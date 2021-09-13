using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITestApp.Services
{
    class BulkPostcodeService
    {
        #region Properties
        //RestSharp Object that handles comms with the API client
        public RestClient Client;
        public JObject ResponseContent { get; set; }
        //The postcode used in this API request
        public JObject Postcodes { get; set; }
        public int StatusCode { get; set; }

        public BulkPostcodeResponse ResponseObject { get; set; }
        #endregion

        public BulkPostcodeService()
        {
            Client = new RestClient { BaseUrl = new Uri(AppConfigReader.PostcodesUrl) };
        }

        public async Task MakeRequest(string[] postcodes)
        {
            //set up request
            var request = new RestRequest();
            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");
            Postcodes = new JObject
            {
                new JProperty("postcodes",
                new JArray(postcodes))
            };

            //define the request resource path
            request.AddParameter("application/json", Postcodes.ToString(), ParameterType.RequestBody);


            //Make request, (doesn't instantiate the interface. Just uses as type
            IRestResponse response = await Client.ExecuteAsync(request);
            
            //Parse JSON in response content
            ResponseContent = JObject.Parse(response.Content);

            StatusCode = (int)response.StatusCode;

            //Parse JSOn string into an object tree
            ResponseObject = JsonConvert.DeserializeObject<BulkPostcodeResponse>(response.Content);
        }
    }
}
