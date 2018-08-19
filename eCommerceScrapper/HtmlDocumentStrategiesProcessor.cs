using eCommerceScrapper;
using eCommerceScrapper.Interfaces;
using HtmlAgilityPack;
using System;
using System.Net.Http;

public class StrategiesProcessorOnHtmlDocument<T> : StrategiesProcessorBase<T> where T : IParseHtmlStrategy
{
    public StrategiesProcessorOnHtmlDocument (IParseStrategiesProvider<T> strategiesProvider, HttpClient httpClient) : base(strategiesProvider, httpClient)
    {
    }

    public HtmlNode Process (HtmlDocument htlmDocument)
    {
        throw new NotImplementedException();
    }
}