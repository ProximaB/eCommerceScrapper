using System.Collections.Generic;

    public class EbayStrategiesProvider : IParseStrategiesProvider
    {
        public List<IParseHtmlStrategy> Strategies => new List<IParseHtmlStrategy>()
        {
            {
                new EbayStrategyUnMinify()
            },
            {
                new EbayStrategyMinify()
            }
        };
    }

