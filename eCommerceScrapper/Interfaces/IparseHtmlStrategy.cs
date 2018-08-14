using HtmlAgilityPack;

namespace eCommerceScrapper.Interfaces
{
    public interface IParseHtmlStrategy
    {
        bool TryCompute (HtmlDocument htmlDocument, out HtmlNode result);

        //bool TryCompute (HtmlDocument htmlDoc);

        //HtmlNode Result { get; }
    }
}