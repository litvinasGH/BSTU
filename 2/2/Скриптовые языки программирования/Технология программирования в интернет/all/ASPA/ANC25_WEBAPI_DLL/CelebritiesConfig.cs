namespace ANC25_WEBAPl_DLL
{
    public class CelebritiesConfig
    {
        public string PhotosFolder { get; set; }
        public string ConnectionString { get; set; }
        public string PhotosRequestPath { get; set; }
        public string ISO3166alpha2Path { get; set; }

        public CelebritiesConfig()
        {
            this.PhotosRequestPath = "/Photos";
            this.PhotosFolder = "./Photos";
            this.ConnectionString = "Data source=SOURCE;Initial Catalog=DBNAME;User Id=USERLOGIN;Password=PASSWORD";
            this.ISO3166alpha2Path = "/CountryCodes/Codes.json";
        }
    }
}