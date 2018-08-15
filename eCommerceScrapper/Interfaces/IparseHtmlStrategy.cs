using HtmlAgilityPack;

namespace eCommerceScrapper.Interfaces
{
    public interface IParseHtmlStrategy
    {
        HtmlNode Compute (string url);

        //bool TryCompute (string url, out HtmlNode result);

        //bool TryCompute (HtmlDocument htmlDoc);

        //HtmlNode Result { get; }
    }
}