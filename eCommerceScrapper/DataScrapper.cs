using eCommerceScrapper.Interfaces;
using HtmlAgilityPack;

namespace eCommerceScrapper
{
    public class DataScrapper<T> where T : IParseHtmlStrategy 
    {
        private readonly IParseStrategiesProvider<T> _strategiesProvider;

        /*TODO: Make HtmlParser with generic type out if possible, returnet type shoud be provided by concrete strategy*/
        /*TODO: Dla ebay strategy model zawsze powinien byc taki sam. */
        /* We can use generic type return public T foo<T>() */

        public DataScrapper (IParseStrategiesProvider<T> strategiesProvider)
        {
            _strategiesProvider = strategiesProvider;
        }

        public bool TryParsePage (string url, out HtmlNode htmlNode)
        {
            foreach ( var strategy in _strategiesProvider.Strategies )
            {
                var result = strategy.Compute(url);
                if ( result != null )
                {
                    htmlNode = result;
                    return true;
                }
            }

            htmlNode = null;
            return false;
        }
    }
}