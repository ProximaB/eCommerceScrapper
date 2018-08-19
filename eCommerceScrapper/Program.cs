using eCommerceScrapper.Interfaces;
using eCommerceScrapper.StrategyProvider;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;

namespace eCommerceScrapper
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }

        public static IConfiguration ConfigurationProvider ()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return builder.Build();
        }

        public static void Main (string[] args = null)
        {
            Configuration = ConfigurationProvider();

            Console.WriteLine($"ebay = {Configuration["urls:ebayUrls:1:title"]}");
            var ebayUrl1 = Configuration["urls:ebayUrls:1:url"];
            var ebayUrl2 = Configuration["urls:ebayUrls:2:url"];
            var webhook = "https://webhook.site/87a3e69a-ead4-43ca-91da-0d091c1a68d8";
            var strategies = new EbayStrategiesProvider();
            var strategiesProcessorBase = new StrategiesProcessorBase<IEbayStrategy>(strategies, new HttpClient());
            var result = strategiesProcessorBase.Process(Configuration["urls:ebayUrls:0:url"], false);

            Console.WriteLine(result.InnerHtml);

            //var url = Configuration["urls:ebayUrls:1:url"];

            //var httpClientHandler = new HttpClientHandler();
            //var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0");
            //var strategies = new EbayStrategiesProvider(httpClient);

            //var parser = new DataScrapper<>(strategies);

            //if ( parser.TryParsePage(url, out HtmlNode htmlNode) == true )
            //{
            //    Console.WriteLine("Resultat Products Htmlparser");
            //    Console.WriteLine(htmlNode.InnerText);
            //}
        }
    }
}