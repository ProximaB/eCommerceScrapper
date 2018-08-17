using eCommerceScrapper.Interfaces;
using HtmlAgilityPack;
using System.Linq;
using System.Net.Http;

namespace eCommerceScrapper.ParseHtmlStrategies.EbayStrategies
{
    public class EbayStrategyMinify : ParseHtmlStrategy, IEbayStrategy
    {
        protected override HtmlNode Parser (HtmlDocument htmlDocument)
        {
            HtmlNode productListHtml = htmlDocument.DocumentNode
                .Descendants("ul").FirstOrDefault(node =>
                    node.GetAttributeValue("class", "").Equals("srp-results srp-list clearfix"));
            productListHtml?.SelectSingleNode("//div[@id='srp-river-results-SEARCH_STATUS_MODEL_V2']").Remove();
            //productListHtml?.SelectSingleNode("//div[@id='srp-river-results-query_answer1']").Remove();
            //productListHtml?.SelectSingleNode("//div[@id='srp-river-results-SEARCH_PAGINATION_MODEL_V2']").Remove();

            return productListHtml;
        }

        protected override void PreRequestAction (HttpRequestMessage request)
        {
            request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Linux; U; " +
                                                    "Android 4.0.2; en-us; Galaxy Nexus Build/ICL53F) " +
                                                    "AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0" +
                                                    " Mobile Safari/534.30");
        }

        protected override bool UrlValid (string url)
        {
            return true;
        }

        public EbayStrategyMinify (HttpClient httpClient) : base(httpClient)
        {
        }
    }
}