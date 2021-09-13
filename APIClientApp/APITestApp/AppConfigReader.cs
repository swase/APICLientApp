using System.Configuration; //Allows us to access the app.config file

namespace APITestApp
{
    class AppConfigReader
    {
        public static readonly string BaseUrl = ConfigurationManager.AppSettings["base_url"];
        public static readonly string PostcodesUrl = ConfigurationManager.AppSettings["postcodes_url"];
    }
}
