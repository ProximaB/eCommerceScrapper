﻿using eCommerceScrapper.Interfaces;
using HtmlAgilityPack;
using System.Net.Http;

namespace eCommerceScrapper
{
    public class ProductsHtmlParser
    {
        private readonly IParseStrategiesProvider _strategiesProvider;
        private readonly HtmlDocument _htmlDocument;
        private readonly HttpClient _httpClient = new HttpClient();

        /*TODO: Make HtmlParser with generic type out if possible, returnet type shoud be provided by concrete strategy*/
        /*TODO: Dla ebay strategy model zawsze powinien byc taki sam. */
        /* We can use generic type return public T foo<T>() */

        public ProductsHtmlParser (IParseStrategiesProvider strategiesProvider)

        {
            _strategiesProvider = strategiesProvider;
            //var htmlSteam = _httpClient.GetStreamAsync(url).Result;
            //_htmlDocument = new HtmlDocument();
            //_htmlDocument.Load(htmlSteam);
        }

        public bool TryParsePage (string url, out HtmlNode htmlNode)
        {
            var htmlSteam = _httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlSteam);

            foreach ( var strategy in _strategiesProvider.Strategies )
            {
                if ( strategy.TryCompute(htmlDocument, out HtmlNode result) )
                {
                    htmlNode = result;
                    return true;
                }
            }

            htmlNode = null;
            return false;

            //htmlNode = null;
            //foreach (var strategy in _strategiesProvider.Strategies)
            //{
            //    if (strategy.TryCompute(htmlDocument, out  htmlNode) )
            //    {
            //        Console.WriteLine($"ItemList htmlParser used: {strategy.GetType().Name}");
            //        break;
            //    }
            //    Console.WriteLine($"Couldn't ParseHtmlStrategy html  {strategy.GetType().Name}");
            //}

            //return htmlNode != null;
        }
    }
}