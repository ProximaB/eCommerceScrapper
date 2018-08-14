using System.Collections.Generic;

namespace eCommerceScrapper.Interfaces
{
    public interface IParseStrategiesProvider
    {
        List<IParseHtmlStrategy> Strategies { get; }
    }
}