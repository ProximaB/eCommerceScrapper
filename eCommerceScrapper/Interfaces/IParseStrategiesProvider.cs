using System.Collections.Generic;

namespace eCommerceScrapper.Interfaces
{
    public interface IParseStrategiesProvider<T> where T : IParseStrategy
    {
        List<T> Strategies { get; }
    }
}