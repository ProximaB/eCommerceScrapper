﻿using eCommerceScrapper.Interfaces;
using HtmlAgilityPack;
using System.Linq;
using System.Net.Http;

namespace eCommerceScrapper.ParseHtmlStrategies.EbayStrategies
{
    public class EbayStrategyUnMinify : IEbayStrategy

    {
        public HtmlNode Parser (HtmlDocument htmlDocument)
        {
            var productListHtml = htmlDocument.DocumentNode
                .Descendants("ul").FirstOrDefault(node =>
                    node.GetAttributeValue("id", "") == "ListViewInner");
            return productListHtml;
        }

        public void PreRequestAction (HttpRequestMessage request)
        {
            request.Headers.TryAddWithoutValidation("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36");
        }

        public bool IsUrlValid (string url)
        {
            return true;
        }
    }
}