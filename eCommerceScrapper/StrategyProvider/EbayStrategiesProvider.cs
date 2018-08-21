using eCommerceScrapper.Interfaces;
using eCommerceScrapper.ParseHtmlStrategies.EbayStrategies;
using System.Collections.Generic;

namespace eCommerceScrapper.StrategyProvider
{
    public class EbayStrategiesProvider : IParseStrategiesProvider<IEbayStrategy>
    {
        public IEnumerable<IEbayStrategy> Strategies { get; }

        public EbayStrategiesProvider (IEnumerable<IEbayStrategy> ebayStrategies)
        {
            Strategies = ebayStrategies;
        }
    }
}