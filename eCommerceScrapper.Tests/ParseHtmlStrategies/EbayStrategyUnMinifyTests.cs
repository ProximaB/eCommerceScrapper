using System.Net.Http;
using eCommerceScrapper.ParseHtmlStrategies.EbayStrategies;
using HtmlAgilityPack;
using Xunit;

namespace eCommerceScrapper.Tests.ParseHtmlStrategies
{
    public class EbayStrategyUnMinifyTests
    {
        public EbayStrategyUnMinify Subject ()
        {
            return new EbayStrategyUnMinify();
        }

        // MethodName_StateUnderTest_ExpectedBehavior
        //Two assert https://softwareengineering.stackexchange.com/questions/267204/how-do-you-unit-test-a-function-that-clears-properties
        // https://softwareengineering.stackexchange.com/questions/7823/is-it-ok-to-have-multiple-asserts-in-a-single-unit-test
        [Fact]
        public void Parser_WithEmptyHtmlDocument_ResultIsNull ()
        {
            //Arange
            var strategy = Subject();
            var emptyDocument = new HtmlDocument();

            //Act
            var result = strategy.Parser(emptyDocument);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Parser_WithValidHtmlDocument_ResultNotNull ()
        {
            //Arange
            var strategy = Subject();
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml("<ul id=\"ListViewInner\"/>");

            //Act
            var result = strategy.Parser(htmlDocument);

            //Assert
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