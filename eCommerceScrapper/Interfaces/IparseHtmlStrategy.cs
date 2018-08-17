using HtmlAgilityPack;
using System.Net.Http;

namespace eCommerceScrapper.Interfaces
{
    public interface IParseHtmlStrategy
    {
        HtmlNode Compute(string url);

        //HtmlNode Parse(HtmlDocument);


    }
}