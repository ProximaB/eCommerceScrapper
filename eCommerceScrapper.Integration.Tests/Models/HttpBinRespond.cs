namespace eCommerceScrapper.Integration.Tests.Models
{
    public class Args
    {
    }

    public class Headers
    {
        public string Accept { get; set; }

        [Newtonsoft.Json.JsonProperty("Accept-Encoding")]
        public string Accept_Encoding { get; set; }

        [Newtonsoft.Json.JsonProperty("Accept-Language")]
        public string Accept_Language { get; set; }

        public string Connection { get; set; }
        public string Cookie { get; set; }
        public string Host { get; set; }
        public string Referer { get; set; }

        [Newtonsoft.Json.JsonProperty("User-Agent")]
        public string User_Agent { get; set; }
    }

    public class HttpBinRespond
    {
        public Args args { get; set; }
        public Headers headers { get; set; }
        public string origin { get; set; }
        public string url { get; set; }
    }
}