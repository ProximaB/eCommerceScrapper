using HtmlAgilityPack;

namespace eCommerceScrapper
{
    public class ProductsHtmlParser
    {
        private readonly IParseStrategiesProvider _strategiesProvider;

        /*TODO: Make HtmlParser with generic type out if possible, returnet type shoud be provided by concrete strategy*/
        /*TODO: Dla ebay strategy model zawsze powinien byc taki sam. */
        /* We can use generic type return public T foo<T>() */

        public ProductsHtmlParser (IParseStrategiesProvider strategiesProvider)
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