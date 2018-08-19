using HtmlAgilityPack;

namespace eCommerceScrapper.Interfaces
{
    public interface IStrategiesProcessor
    {
        HtmlNode Process (string url, bool urlValidation = false);
    }
}