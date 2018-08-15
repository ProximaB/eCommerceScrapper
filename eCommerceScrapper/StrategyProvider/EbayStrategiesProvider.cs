using eCommerceScrapper.Interfaces;
using eCommerceScrapper.ParseHtmlStrategies;
using System.Collections.Generic;
using System.Net.Http;

namespace eCommerceScrapper.StrategyProvider
{
    public class EbayStrategiesProvider : IParseStrategiesProvider
    {
        private readonly HttpClient _httpClient;

        public EbayStrategiesProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<IParseHtmlStrategy> Strategies =>
            new List<IParseHtmlStrategy>()
            {
                new EbayStrategyUnMinify(_httpClient),
                new EbayStrategyMinify(_httpClient)
            };
    }
}