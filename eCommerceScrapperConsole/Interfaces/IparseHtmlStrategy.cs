using HtmlAgilityPack;

public interface IParseHtmlStrategy
{
    bool Compute(HtmlDocument htmlDoc);
    HtmlNode Result { get; }
}