using HtmlAgilityPack;
using System;
using System.Net.Http;

public partial class Program
{
    public class ProductsHtmlParser
    {
        private readonly IParseStrategiesProvider _strategiesProvider;
        private readonly HtmlDocument _htmlDocument;
        private readonly HttpClient _httpClient = new HttpClient();

        public ProductsHtmlParser (IParseStrategiesProvider strategiesProvider)
        {
            _strategiesProvider = strategiesProvider;
            //var htmlSteam = _httpClient.GetStreamAsync(url).Result;
            //_htmlDocument = new HtmlDocument();
            //_htmlDocument.Load(htmlSteam);
        }

        public bool TryParsePage(string url, out HtmlNode htmlNode)
        {
            var htmlSteam = _httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlSteam);

            htmlNode = null;

            foreach (var strategy in _strategiesProvider.Strategies)
            {
                
                if ( strategy.Compute(htmlDocument) )
                {
                    Console.WriteLine($"ItemList htmlParser used: {strategy.GetType().Name}");
                    htmlNode = strategy.Result;
                    break;
                }
                else
                {
                    Console.WriteLine($"Couldn't ParseHtmlStrategy html  {strategy.GetType().Name}");
                }
            }

            return htmlNode != null;
        }
    }

}
