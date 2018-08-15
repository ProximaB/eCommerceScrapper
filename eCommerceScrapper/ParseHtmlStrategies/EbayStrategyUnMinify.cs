using HtmlAgilityPack;
using System.Linq;
using System.Net.Http;

namespace eCommerceScrapper.ParseHtmlStrategies
{
    public class EbayStrategyUnMinify : ParseHtmlStrategy

    {
        protected override HtmlNode Parser (string url)
        {
            var htmlDocument = GetHtmlResponse (url, null); //TODO: find way to change user agent
            HtmlNode productListHtml = htmlDocument.DocumentNode
                .Descendants("ul").FirstOrDefault(node =>
                    node.GetAttributeValue("id", "").Equals("ListViewInner"));
            return productListHtml;
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