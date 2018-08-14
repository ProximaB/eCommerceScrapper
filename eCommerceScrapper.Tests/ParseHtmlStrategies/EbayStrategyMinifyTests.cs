using Moq;
using System;
using Xunit;
using eCommerceScrapper.ParseHtmlStrategies;
using HtmlAgilityPack;

namespace eCommerceScrapper.Tests.ParseHtmlStrategies
{
    public class EbayStrategyMinifyTests
    {

        public EbayStrategyMinifyTests ()
        {
        }

        private EbayStrategyMinify Subject ()
        {
            return new EbayStrategyMinify();
        }

        [Fact]
        public void TryCompute_WithEmptyHtmlDocument_ReturnFalseResultIsNull ()
        {
            // Arrange
            var strategy = Subject();
            var htmlDocument = new HtmlDocument();

            // Act
            var success = strategy.TryCompute(htmlDocument, out var result);

            // Assert
            Assert.False(success);
            Assert.Null(result);
        }

        [Fact]
        public void TryCompute_WithValidHtmlDocument_ReturnTrueResultNotNull()
        {
            // Arrange
            var strategy = Subject();
            var htmlDocument = new HtmlDocument();
            const string html = "<ul class=\"srp-results srp-list clearfix\">" +
                                  "<div id=\"srp-river-results-SEARCH_STATUS_MODEL_V2\"/>" +
                                "</ul>";
            htmlDocument.LoadHtml(html);

            //Act
            var success = strategy.TryCompute(htmlDocument, out var result);

            // Assert
            Assert.True(success);
            Assert.NotNull(result);
        }
    }
}
