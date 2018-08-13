using System.Collections.Generic;

 public interface IParseStrategiesProvider
{
    List<IParseHtmlStrategy> Strategies { get; }
}

