using Newtonsoft.Json;

namespace APITestApp.PostcodeIOService.DataHandling
{
    //Constraints specify that the DTO can only be of IResponse type AND new keyword
    //is used. 
    public class DTO<ResponseType> where ResponseType : IResponse, new()
    {
        //A propert which represents the model
        public ResponseType Response { get; set; }
        
        //Method that creates the above object using the response

        public void Deserialize(string postcodeResponse)
        {
            Response = JsonConvert.DeserializeObject<ResponseType>(postcodeResponse);
        }

    }
}
