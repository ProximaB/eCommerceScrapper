using System;
using eCommerceScrapper.Interfaces;
using HtmlAgilityPack;
using System.Net.Http;
using eCommerceScrapper.Extensions;
namespace eCommerceScrapper
{
    public class StrategiesProcessorBase<T> : IStrategiesProcessor where T : IParseHtmlStrategy
    {
        private readonly IParseStrategiesProvider<T> _strategiesProvider;
        private string _url;
        private bool _urlValidation;
        private readonly HttpClient _httpClient;

        public StrategiesProcessorBase (IParseStrategiesProvider<T> strategiesProvider, HttpClient httpClient)
        {
            _strategiesProvider = strategiesProvider;
            _httpClient = httpClient;
        }

        //TODO HtmlNode to generic T
        public HtmlNode Process (string url, bool urlValidation = false)
        {
            _url = url;
            _urlValidation = urlValidation;

            foreach (var strategy in _strategiesProvider.Strategies)
            {
                var result = ComputeStrategy(strategy);
                if (result != null)
                {
                    return result;
                }
                Console.WriteLine($"Strategy {strategy.GetType().Name} unable to parse.");
            }
            Console.WriteLine("Exit StrategiesProcessor.Process with null");
            return null;
        }

        private HtmlNode ComputeStrategy (IParseHtmlStrategy strategy)
        {
            if ( _urlValidation && !strategy.IsUrlValid(_url) )
                return null;

            var htmlDocument = GetHtmlResponse(strategy);
            var productListHtml = strategy.Parser(htmlDocument);
            return productListHtml;
        }

        private HtmlDocument GetHtmlResponse (IParseHtmlStrategy strategy)// Action<HttpRequestMessage> preRequestAction)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _url);

            strategy.PreRequestAction(request);

            var htmlString = _httpClient.GetStringAsync(_url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);
            return htmlDocument;
        }
    }
}