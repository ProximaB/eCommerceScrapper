using eCommerceScrapper.Interfaces;
using eCommerceScrapper.ParseHtmlStrategies;
using System.Collections.Generic;

namespace eCommerceScrapper.StrategyProvider
{
    public class EbayStrategiesProvider : IParseStrategiesProvider
    {
        public List<IParseHtmlStrategy> Strategies =>
            new List<IParseHtmlStrategy>()
            {
                new EbayStrategyUnMinify(),
                new EbayStrategyMinify()
            };
    }
}