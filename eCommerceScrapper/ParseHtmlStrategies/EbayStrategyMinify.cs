using HtmlAgilityPack;
using System.Linq;
using System.Net.Http;

namespace eCommerceScrapper.ParseHtmlStrategies
{
    public class EbayStrategyMinify : ParseHtmlStrategy
    {
        protected override HtmlNode Parser (string url)
        {
            var htmlDocument = GetHtmlResponse(url, null); //TODO: find way to change user agent
            HtmlNode productListHtml = htmlDocument.DocumentNode
                .Descendants("ul").FirstOrDefault(node =>
                    node.GetAttributeValue("class", "").Equals("srp-results srp-list clearfix"));
            productListHtml?.SelectSingleNode("//div[@id='srp-river-results-SEARCH_STATUS_MODEL_V2']").Remove();
            //productListHtml?.SelectSingleNode("//div[@id='srp-river-results-query_answer1']").Remove();
            //productListHtml?.SelectSingleNode("//div[@id='srp-river-results-SEARCH_PAGINATION_MODEL_V2']").Remove();

            return productListHtml;
        }

        protected override bool UrlValid(string url)
        {
            return true;
        }

        public EbayStrategyMinify(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}