using System;
using eCommerceScrapper.ParseHtmlStrategies.EbayStrategies;
using HtmlAgilityPack;
using RichardSzalay.MockHttp;
using System.Net.Http;
using eCommerceScrapper.Interfaces;
using Moq;
using Xunit;

namespace eCommerceScrapper.Tests.ParseHtmlStrategies
{
    public class EbayStrategyMinifyTests
    {
        private EbayStrategyMinify Subject ()
        {
            //var mockHttpMessageHandler = new MockHttpMessageHandler();
            //mockHttpMessageHandler.When(@"http://testUrl").Respond("text/html", "<ul id=\"ListViewInner\"/>");
            //var mockhttpClient = new HttpClient(mockHttpMessageHandler);
            return new EbayStrategyMinify();
        }

        [Fact]
        public void Parser_WithEmptyHtmlDocument_ResultIsNull ()
        {
            // Arrange
            var strategy = Subject();
            var emptyDocument = new HtmlDocument();

            // Act
            var result = strategy.Parser(emptyDocument);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Parser_WithValidHtmlDocument_ResultNotNull ()
        {
            // Arrange
            var strategy = Subject();
            var htmlDocument = new HtmlDocument();
            const string html = "<ul class=\"srp-results srp-list clearfix\">" +
                                "<div id=\"srp-river-results-SEARCH_STATUS_MODEL_V2\"/>" +
                                "</ul>";
            htmlDocument.LoadHtml(html);

            //Act
            var result = strategy.Parser(htmlDocument);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void IsUrlValid_PassingValidUrlForStrategy_ResultTrue ()
        {
            // Arrange
            var strategy = Subject();

            //Act
            var result = strategy.IsUrlValid("http://www.ebay.com/");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsUrlValid_PassingInvalidUrlForStrategy_ResultFalse ()
        {
            // Arrange
            var strategy = Subject();

            //Act
            var result = strategy.IsUrlValid("http://www.InvalidUrl.com/");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void PreRequestAction_CheckingIfHttpRequestMessageModifiedSuccessfully_ResultTrue ()
        {
            // Arrange
            var strategy = Subject();
            var httpRequestMessage = new HttpRequestMessage();
            //Act
            var result = strategy.PreRequestAction(httpRequestMessage);
            // Assert
            Assert.True(result);
        }
    }
}