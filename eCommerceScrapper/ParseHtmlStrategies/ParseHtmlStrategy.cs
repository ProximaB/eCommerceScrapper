using eCommerceScrapper.Interfaces;
using HtmlAgilityPack;
using System;
using System.Net.Http;

namespace eCommerceScrapper.ParseHtmlStrategies
{
    //Brainstorm: Extend ParseHtmlStrategy for validating throu incoming request, if not don't fire Parse, but go to next Strategy
    //Brainstorm: Let Strategy obtain specyfic User-Agent, so it's always get right html, many startegies ensure that reuslt will be supplied if html views for some user-agent would change.
    //Brainstorm: So if there is validation (like pipeline) we can assume that every UserAgent has only one type of html view, for specyfic url, there fore its no need to grup by user-agen for decrese http request.
    //TODO: ParseHthmlStrategy shuld be injectable by httpclient instance, then supply it with OWN UserAgent (achive by DefaultRequestHeaders.UserAgent), provide Validation method on url that will eventauly tryParse or move to the next strategy.
    public abstract class ParseHtmlStrategy : IParseHtmlStrategy
    {
        private readonly HttpClient _httpClient;

        protected ParseHtmlStrategy (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected abstract HtmlNode Parser (string url);
        protected abstract HttpRequestMessage HttpRequestMessage ();

        protected abstract bool UrlValid (string url);

        public HtmlNode Compute (string url)
        {
            if ( !UrlValid(url) )
                return null;

            var productListHtml = Parser(url);
            return productListHtml;
        }

        protected HtmlDocument GetHtmlResponse (string url, Action<HttpRequestMessage> preAction)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Linux; U; " +
            "Android 4.0.2; en-us; Galaxy Nexus Build/ICL53F) AppleWebKit/534.30 (KHTML," +
            " like Gecko) Version/4.0 Mobile Safari/534.30");

            var htmlString = _httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);

            return htmlDocument; 
        }

        //public bool TryCompute (string url, out HtmlNode result)
        //{
        //    if (!UrlValid(url))
        //    {
        //        result = null;
        //        return false;
        //    }

        //    var productListHtml = Parser(url);

        //    if ( productListHtml == null )
        //    {
        //        result = null;
        //        return false;
        //    }

        //    result = productListHtml;
        //    return true;
        //}
    }
}