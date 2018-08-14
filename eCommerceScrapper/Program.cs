using eCommerceScrapper.StrategyProvider;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace eCommerceScrapper
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }

        public static IConfiguration configurationProvider ()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return builder.Build();
        }

        public static void Main (string[] args = null)
        {
            Configuration = configurationProvider();

            Console.WriteLine($"ebay = {Configuration["urls:ebayUrls:2:title"]}");

            var url = Configuration["urls:ebayUrls:1:url"];
            var strategies = new EbayStrategiesProvider();

            var parser = new ProductsHtmlParser(strategies);
            if ( parser.TryParsePage(url, out HtmlNode htmlNode) == true )
            {
                Console.WriteLine("Resultat Products Htmlparser");
                Console.WriteLine(htmlNode.InnerText);
            }
        }
    }
}