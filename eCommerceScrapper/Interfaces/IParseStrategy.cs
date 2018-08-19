using HtmlAgilityPack;
using System.Net.Http;

namespace eCommerceScrapper.Interfaces
{
    public interface IParseStrategy
    {
        HtmlNode Parser (HtmlDocument htmlDocument);

        bool IsUrlValid (string url);

        bool PreRequestAction (HttpRequestMessage request);
    }
}