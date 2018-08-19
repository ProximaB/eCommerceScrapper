using System.Net.Http;
using HtmlAgilityPack;

namespace eCommerceScrapper.Interfaces
{
    public interface IParseHtmlStrategy
    {
        HtmlNode Parser (HtmlDocument htmlDocument);

        bool IsUrlValid(string url);

        void PreRequestAction(HttpRequestMessage request);
    }
}