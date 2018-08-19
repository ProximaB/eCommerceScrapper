using HtmlAgilityPack;

namespace eCommerceScrapper
{
    public interface IStrategiesProcessor
    {
        HtmlNode Process (string url, bool urlValidation = false);
    }
}