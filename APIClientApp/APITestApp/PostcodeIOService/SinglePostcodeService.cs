using APITestApp.PostcodeIOService.DataHandling;
using APITestApp.PostcodeIOService.HTTPManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace APITestApp.PostcodeIOSerice
{
    public class SinglePostcodeService
    {
        #region Properties
        public CallManager CallManager { get; set; }
        public JObject Json_Response { get; set; }
        public DTO<SinglePostcodeResponse> SinglePostcodeDTO { get; set; }
        public string PostcodeSelected { get; set; }
        public string PostcodeResponse { get; set; }
        #endregion

        public SinglePostcodeService()
        {
            CallManager = new CallManager();
            SinglePostcodeDTO = new DTO<SinglePostcodeResponse>();

        }

        public async Task MakeRequest(string postcode)
        {
            PostcodeResponse = await CallManager.MakePostcodeRequestAsync(postcode);
            PostcodeSelected = postcode;
            Json_Response = JObject.Parse(PostcodeResponse);
            SinglePostcodeDTO.Deserialize(PostcodeResponse);
        }



        public int CodeCount()
        {
            //return SinglePostcodeDTO.Response.result.codes.;
            var count = 0;
            foreach (var code in Json_Response["result"]["codes"])
            {
                count++;
            }

            return count;
        }




    }
}
