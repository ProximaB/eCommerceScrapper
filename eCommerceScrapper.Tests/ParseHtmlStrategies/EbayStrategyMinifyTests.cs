using eCommerceScrapper.ParseHtmlStrategies.EbayStrategies;
using HtmlAgilityPack;
using RichardSzalay.MockHttp;
using System.Net.Http;
using Xunit;

namespace eCommerceScrapper.Tests.ParseHtmlStrategies
{
    public class EbayStrategyMinifyTests
    {
        private EbayStrategyMinify Subject ()
        {
            var mockHttpMessageHandler = new MockHttpMessageHandler();
            mockHttpMessageHandler.When(@"http://testUrl").Respond("text/html", "<ul id=\"ListViewInner\"/>");
            var mockhttpClient = new HttpClient(mockHttpMessageHandler);
            return new EbayStrategyMinify(mockhttpClient);
        }

        [Fact]
        public void TryCompute_WithEmptyHtmlDocument_ReturnFalseResultIsNull ()
        {
            // Arrange
            var strategy = Subject();
            var url = "";

            // Act
            var result = strategy.Compute(url);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void TryCompute_WithValidHtmlDocument_ReturnTrueResultNotNull ()
        {
            // Arrange
            var strategy = Subject();
            var htmlDocument = new HtmlDocument();
            const string html = "<ul class=\"srp-results srp-list clearfix\">" +
                                "<div id=\"srp-river-results-SEARCH_STATUS_MODEL_V2\"/>" +
                                "</ul>";
            htmlDocument.LoadHtml(html);

            //Act
            var success = strategy.Compute("http://test/");

            // Assert
            Assert.True(success);
            Assert.NotNull(result);
        }
    }
}