using eCommerceScrapper.Interfaces;
using HtmlAgilityPack;

namespace eCommerceScrapper.ParseHtmlStrategies
{
    public abstract class ParseHtmlStrategy : IParseHtmlStrategy
    {
        //public HtmlNode Result { get; private set; }

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

        //public bool TryCompute (HtmlDocument htmlDoc)
        //{
        //    var productListHtml = Parser(htmlDoc);

        //    if ( productListHtml == null )
        //    {
        //        return false;
        //    }

        //    Result = productListHtml;
        //    return true;
        //}
    }
}