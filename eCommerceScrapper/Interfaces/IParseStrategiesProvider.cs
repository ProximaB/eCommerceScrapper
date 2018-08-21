using System.Collections.Generic;

namespace eCommerceScrapper.Interfaces
{
    public interface IParseStrategiesProvider<T> where T : IParseStrategy
    {
        IEnumerable<T> Strategies { get; }
    }
}