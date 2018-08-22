using HtmlAgilityPack;
using System.Net.Http;

namespace eCommerceScrapper.Interfaces
{
    public interface IParseStrategy//TODO <T> where T : class
    {
        HtmlNode Parser (HtmlDocument htmlDocument);

        bool IsUrlValid (string url);

        bool PreRequestAction (HttpRequestMessage request);
    }
}