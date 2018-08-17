using HtmlAgilityPack;
using System.Linq;
using System.Net.Http;

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

        protected override void PreRequestAction(HttpRequestMessage request)
        {
            request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Linux; U; " +
                                                                  "Android 4.0.2; en-us; Galaxy Nexus Build/ICL53F) " +
                                                                  "AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0" +
                                                                  " Mobile Safari/534.30");
        }

        protected override bool UrlValid(string url)
        {
            return true;
        }

        public EbayStrategyUnMinify(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}