using eCommerceScrapper.Interfaces;
using HtmlAgilityPack;
using System;
using System.Net.Http;

namespace eCommerceScrapper
{
    public class StrategiesProcessorBase<T> : IStrategiesProcessor where T : IParseStrategy
    {
        private readonly HttpClient _httpClient;
        private readonly IParseStrategiesProvider<T> _strategiesProvider;
        private string _url;
        private bool _urlValidation;

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

            foreach ( var strategy in _strategiesProvider.Strategies )
            {
                var result = ComputeStrategy(strategy);
                if ( result != null )
                {
                    Console.WriteLine($"Parsed using {strategy.GetType().Name} strategy.");
                    return result;
                }

                Console.WriteLine($"Strategy {strategy.GetType().Name} unable to parse.");
            }

            Console.WriteLine("Exit StrategiesProcessor.Process with null");
            return null;
        }

        private HtmlNode ComputeStrategy (IParseStrategy strategy)
        {
            if ( _urlValidation && !strategy.IsUrlValid(_url) )
            {
                return null;
            }

            var htmlDocument = GetHtmlResponse(strategy);
            var productListHtml = strategy.Parser(htmlDocument);
            return productListHtml;
        }

        private HtmlDocument GetHtmlResponse (IParseStrategy strategy) // Action<HttpRequestMessage> preRequestAction)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _url);

            strategy.PreRequestAction(request);

            var htmlString = _httpClient.SendAsync(request).Result.Content.ReadAsStringAsync().Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);
            return htmlDocument;
        }
    }
}