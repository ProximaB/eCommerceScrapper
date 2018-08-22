using eCommerceScrapper;
using eCommerceScrapper.Interfaces;
using HtmlAgilityPack;
using System;
using System.Net.Http;

//TODO Processor for url or htmldocument.
public class EbayStrategiesProcessor : StrategiesProcessorBase<IEbayStrategy>
{
    public EbayStrategiesProcessor (IParseStrategiesProvider<IEbayStrategy> strategiesProvider, HttpClient httpClient) : base(strategiesProvider, httpClient)
    {
    }
}