using eCommerceScrapper.Interfaces;
using eCommerceScrapper.ParseHtmlStrategies.EbayStrategies;
using System.Collections.Generic;
using System.Net.Http;

namespace eCommerceScrapper.StrategyProvider
{
    public class EbayStrategiesProvider : IParseStrategiesProvider<IEbayStrategy>
    {
        private readonly HttpClient _httpClient;

        public EbayStrategiesProvider (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<IEbayStrategy> Strategies =>
            new List<IEbayStrategy>()
            {
                new EbayStrategyUnMinify(_httpClient),
                new EbayStrategyMinify(_httpClient)
            };
    }
}