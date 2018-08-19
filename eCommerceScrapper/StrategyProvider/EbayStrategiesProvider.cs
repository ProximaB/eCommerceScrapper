using eCommerceScrapper.Interfaces;
using eCommerceScrapper.ParseHtmlStrategies.EbayStrategies;
using System.Collections.Generic;
using System.Net.Http;

namespace eCommerceScrapper.StrategyProvider
{
    public class EbayStrategiesProvider : IParseStrategiesProvider<IEbayStrategy>
    {

        public List<IEbayStrategy> Strategies =>
            new List<IEbayStrategy>()
            {
                new EbayStrategyUnMinify(),
                new EbayStrategyMinify()
            };
    }
}