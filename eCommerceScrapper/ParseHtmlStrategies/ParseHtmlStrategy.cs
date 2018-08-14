using eCommerceScrapper.Interfaces;
using HtmlAgilityPack;

namespace eCommerceScrapper.ParseHtmlStrategies
{
    public abstract class ParseHtmlStrategy : IParseHtmlStrategy
    {
        protected abstract HtmlNode Parser (HtmlDocument htmlDocument);

        public bool TryCompute (HtmlDocument htmlDoc, out HtmlNode result)
        {
            var productListHtml = Parser(htmlDoc);

            if ( productListHtml == null )
            {
                result = null;
                return false;
            }

            result = productListHtml;
            return true;
        }
    }
}