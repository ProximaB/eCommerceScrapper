using System;
using System.Linq;
using HtmlAgilityPack;

class EbayStrategyUnMinify : ParseHtmlStrategy
{
    protected override HtmlNode Parser (HtmlDocument htmlDocument)
    {
        HtmlNode productListHtml = htmlDocument.DocumentNode
                .Descendants("ul").FirstOrDefault(node =>
                    node.GetAttributeValue("id", "").Equals("ListViewInner"));
        return productListHtml;
    }
}