using System.Collections.Generic;

namespace eCommerceScrapper.Interfaces
{
    public interface IParseStrategiesProvider<T> where T : IParseHtmlStrategy
    {
        List<T> Strategies { get; }
    }
}