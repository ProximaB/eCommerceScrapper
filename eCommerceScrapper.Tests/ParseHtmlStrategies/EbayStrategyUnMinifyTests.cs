using eCommerceScrapper.ParseHtmlStrategies;
using HtmlAgilityPack;
using Xunit;

namespace eCommerceScrapper.Tests.ParseHtmlStrategies
{
    public class EbayStrategyUnMinifyTests
    {
        public EbayStrategyUnMinifyTests ()
        {
        }

        public EbayStrategyUnMinify Subject ()
        {
            return new EbayStrategyUnMinify();
        }

        // MethodName_StateUnderTest_ExpectedBehavior
        //Two assert https://softwareengineering.stackexchange.com/questions/267204/how-do-you-unit-test-a-function-that-clears-properties
        // https://softwareengineering.stackexchange.com/questions/7823/is-it-ok-to-have-multiple-asserts-in-a-single-unit-test
        [Fact]
        public void TryCompute_WithEmptyHtmlDocument_ReturnFalseResultIsNull ()
        {
            //Arange
            var strategy = Subject();
            var htmlDocument = new HtmlDocument();

            //Act
            var success = strategy.TryCompute(htmlDocument, out HtmlNode result);

            //Assert
            Assert.False(success);
            Assert.Null(result);
        }

        [Fact]
        public void Compute_WithValidHtmlDocument_ReturnTrueResultIsNotNull ()
        {
            //Arange
            var strategy = Subject();
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml("<ul id=\"ListViewInner\"/>");

            //Act
            var success = strategy.TryCompute(htmlDocument, out HtmlNode result);

            //Assert
            Assert.True(success);
            Assert.NotNull(result);
        }

    }
}