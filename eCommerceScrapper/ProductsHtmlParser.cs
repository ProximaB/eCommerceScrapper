using System.Diagnostics.CodeAnalysis;
using eCommerceScrapper.Interfaces;
using HtmlAgilityPack;
using System.Net.Http;

namespace eCommerceScrapper
{
    public class ProductsHtmlParser
    {
        private readonly HttpClient _httpClient; //TODO: should be replace by inject static or singleton service
        private readonly IParseStrategiesProvider _strategiesProvider;

        /*TODO: Make HtmlParser with generic type out if possible, returnet type shoud be provided by concrete strategy*/
        /*TODO: Dla ebay strategy model zawsze powinien byc taki sam. */
        /* We can use generic type return public T foo<T>() */

        public ProductsHtmlParser (IParseStrategiesProvider strategiesProvider, HttpMessageHandler httpMessageHandler)
        {
            _strategiesProvider = strategiesProvider;
            _httpClient = new HttpClient(httpMessageHandler);
        }

        public bool TryParsePage(string url, out HtmlNode htmlNode)
        {
            var htmlDoc = GetHtmlResponse(url);

            foreach (var strategy in _strategiesProvider.Strategies)
            {
                if (strategy.TryCompute(htmlDoc, out HtmlNode result))
                {
                    htmlNode = result;
                    return true;
                }
            }

            htmlNode = null;
            return false;
        }

        private HtmlDocument GetHtmlResponse (string url)
        {

            var htmlString = _httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);

            return htmlDocument;
        }
    }
}