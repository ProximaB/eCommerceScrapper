using eCommerceScrapper.Interfaces;
using HtmlAgilityPack;
using System;

namespace eCommerceScrapper.ParseHtmlStrategies
{
    public abstract class ParseHtmlStrategy : IParseHtmlStrategy
    {
        public HtmlNode Result { get; private set; }

        protected abstract HtmlNode Parser (HtmlDocument htmlDocument);

        public bool Compute (HtmlDocument htmlDoc)
        {
            try
            {
                var productListHtml = Parser(htmlDoc);

                if ( productListHtml == null )
                {
                    return false;
                }

                Result = productListHtml;
                return true;
            }
            catch ( NullReferenceException )
            {
                return false;
            }
        }
    }
}