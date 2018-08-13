using HtmlAgilityPack;
using System.Linq;

internal class EbayStrategyMinify : ParseHtmlStrategy
{

    protected override HtmlNode Parser(HtmlDocument htmlDocument)
    {
        HtmlNode productListHtml = htmlDocument.DocumentNode
            .Descendants("ul").FirstOrDefault(node =>
                node.GetAttributeValue("class", "").Equals("srp-results srp-list clearfix"));
        productListHtml?.SelectSingleNode("//div[@id='srp-river-results-SEARCH_STATUS_MODEL_V2']").Remove();
        //productListHtml?.SelectSingleNode("//div[@id='srp-river-results-query_answer1']").Remove();
        //productListHtml?.SelectSingleNode("//div[@id='srp-river-results-SEARCH_PAGINATION_MODEL_V2']").Remove();

        return productListHtml;
    }

}