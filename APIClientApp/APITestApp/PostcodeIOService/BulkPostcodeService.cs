using APITestApp.PostcodeIOService.DataHandling;
using APITestApp.PostcodeIOService.HTTPManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITestApp.PostcodeIOSerice
{
    class BulkPostcodeService
    {
        #region Properties
        public CallManager CallManager { get; set; }
        public JObject Json_Response { get; set; }
        public DTO<BulkPostcodeResponse> BulkPostcodeDTO { get; set; }
        public string[] PostcodesSelected { get; set; }
        public string PostcodeResponse { get; set; }
        #endregion

        public BulkPostcodeService()
        {
            CallManager = new CallManager(AppConfigReader.PostcodesUrl);
            BulkPostcodeDTO = new DTO<BulkPostcodeResponse>();
        }
        public async Task MakeRequest(string[] postcodes)
        {
            PostcodeResponse = await CallManager.MakePostcodeRequestAsync(postcodes);
            PostcodesSelected = postcodes;
            Json_Response = JObject.Parse(PostcodeResponse);
            BulkPostcodeDTO.Deserialize(PostcodeResponse);
        }

        public List<string> GetDisctricts()
        {
            var queryForDistricts = BulkPostcodeDTO.Response.result.Select(r => r.result.region).ToList();
            return queryForDistricts;

        }

        public string[] GetPostCodes()
        {
            var postcodes = new List<string>();

            var result = BulkPostcodeDTO.Response.result;
            foreach (var postcode in result)
            {
                postcodes.Add(postcode.result.postcode);
            }

            return postcodes.ToArray();
        }
    }
}
