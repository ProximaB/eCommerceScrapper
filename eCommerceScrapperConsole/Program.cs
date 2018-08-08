using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

public class Program
{
    public static IConfiguration Configuration { get; set; }

    public static IConfiguration configurationProvider ()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        return builder.Build();
    }

    public static void Main (string[] args = null)
    {
        Configuration = configurationProvider();

        Console.WriteLine($"ebay = {Configuration["urls:ebayUrls:0:title"]}");
        // Console.WriteLine($"ebay = {Configuration["urls:ebayUrls:0:url"]}");
        GetHtml();

        Console.WriteLine("Press a key...");
        Console.ReadKey();
    }

    private static void GetHtml ()
    {
        string url = Configuration["urls:ebayUrls:2:url"];

        var httpClient = new HttpClient();
        var html =  httpClient.GetStringAsync(url);
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html.Result);

        var ebaystrategyDictionary = new Dictionary<string, IparseHtmlStrategy>()
        {
            {
                "UnMinify", new EbayStrategyUnMinify()
            },
            {
                "Minify", new EbayStrategyMinify()
            }
        };

        HtmlNode result = null;
        foreach (var strategy in ebaystrategyDictionary.Values)
        {
            strategy.Compute(htmlDocument);
            if (strategy.Result == true)
            {
                result = strategy.productList;
                break;
            }
        }

        Console.WriteLine(result.InnerHtml);
    }
}

internal interface IparseHtmlStrategy
{
    void Compute(HtmlDocument htmlDoc);
    bool Result { get; }

    HtmlNode productList { get; }
}

class EbayStrategyMinify : IparseHtmlStrategy
{
    public HtmlNode productList { get; private set; }
    public bool Result { get; private set; }

    public void Compute (HtmlDocument htmlDoc)
    {
        try
        {
            HtmlNode productListHtml = htmlDoc.DocumentNode
                .Descendants("ul").FirstOrDefault(node =>
                    node.GetAttributeValue("class", "").Equals("srp-results srp-list clearfix"));
            productListHtml?.SelectSingleNode("//div[@id='srp-river-results-SEARCH_STATUS_MODEL_V2']").Remove();
            productListHtml?.SelectSingleNode("//div[@id='srp-river-results-query_answer1']").Remove();
            productListHtml?.SelectSingleNode("//div[@id='srp-river-results-SEARCH_PAGINATION_MODEL_V2']").Remove();

            productList = productListHtml;
            Result = true;
        }
        catch ( NullReferenceException )
        {
            Result = false;
        }
    }
}

class EbayStrategyUnMinify : IparseHtmlStrategy
{
    public HtmlNode productList { get; private set; }
    public bool Result { get; private set; }

    public void Compute (HtmlDocument htmlDoc)
    {
        try
        {
            HtmlNode productListHtml = htmlDoc.DocumentNode
                .Descendants("ul").FirstOrDefault(node =>
                    node.GetAttributeValue("id", "").Equals("ListViewInner"));
            //productListHtml?.SelectSingleNode("//div[@id='srp-river-results-SEARCH_STATUS_MODEL_V2']").Remove();

            productList = productListHtml;
            Result = true;
        }
        catch ( NullReferenceException )
        {
            Result = false;
        }
    }
}

