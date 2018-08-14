using HtmlAgilityPack;
using System.Linq;

namespace eCommerceScrapper.ParseHtmlStrategies
{
    public class EbayStrategyUnMinify : ParseHtmlStrategy

    {
        protected override HtmlNode Parser (HtmlDocument htmlDocument)
        {
            HtmlNode productListHtml = htmlDocument.DocumentNode
                .Descendants("ul").FirstOrDefault(node =>
                    node.GetAttributeValue("id", "").Equals("ListViewInner"));
            return productListHtml;
        }
    }
}