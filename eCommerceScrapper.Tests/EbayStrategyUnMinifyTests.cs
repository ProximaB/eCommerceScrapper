using eCommerceScrapper.ParseHtmlStrategies;
using HtmlAgilityPack;
using Xunit;

namespace eCommerceScrapper.Tests
{
    public class EbayStrategyMinifyTests
    {
        public EbayStrategyMinifyTests ()
        {
        }
        public EbayStrategyUnMinify Subject ()
        {
            return new EbayStrategyUnMinify();
        }

        // MethodName_StateUnderTest_ExpectedBehavior
        //Two assert https://softwareengineering.stackexchange.com/questions/267204/how-do-you-unit-test-a-function-that-clears-properties
        [Fact]
        public void Compute_WithEmptyHtmlDocument_ReturnFalseResultIsNull ()
        {
            //Arange
            var strategy = Subject();
            var htmlDocument = new HtmlDocument();

            //Act
            var result = strategy.Compute(htmlDocument);

            //Assert
            Assert.False(result);
            Assert.Null(strategy.Result);
        }

        [Fact]
        public void Compute_WithValidHtmlDocument_ReturnTrueResultIsNotNull ()
        {
            //Arange
            var strategy = Subject();
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml("<ul id=\"ListViewInner\">qwe</ul>");

            //Act
            var result = strategy.Compute(htmlDocument);

            //Assert
            Assert.True(result);
            Assert.NotNull(strategy.Result);
        }
    }
}