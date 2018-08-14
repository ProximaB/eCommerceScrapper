using HtmlAgilityPack;

namespace eCommerceScrapper.Interfaces
{
    public interface IParseHtmlStrategy
    {
        bool Compute (HtmlDocument htmlDoc);

        HtmlNode Result { get; }
    }
}