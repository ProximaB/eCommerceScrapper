using eCommerceScrapper.Interfaces;
using HtmlAgilityPack;
using Moq;
using RichardSzalay.MockHttp;
using System.Collections.Generic;
using Xunit;

namespace eCommerceScrapper.Tests
{
    public class ProductsHtmlParserTests
    {
        public ProductsHtmlParserTests ()
        {
        }

        private ProductsHtmlParser CreateProductsHtmlParser (IParseStrategiesProvider mockParseStrategiesProvider, MockHttpMessageHandler mockHttpMessageHandler)
        {
            return new ProductsHtmlParser(
                mockParseStrategiesProvider, mockHttpMessageHandler);
        }

        [Fact]
        public void TryParsePage_StrategiesDontMatch_ReturnFalseResultIsNull ()
        {
            // Arrange
            var mockParseHtmlStrategy = new Mock<IParseHtmlStrategy>();
            HtmlNode htmlNode = null;
            mockParseHtmlStrategy.Setup(m => m.TryCompute(new HtmlDocument(), out htmlNode)).Returns(false).Callback(() => htmlNode = null);
            var mockListOfIParseHtmlStartegy = new List<IParseHtmlStrategy>() { mockParseHtmlStrategy.Object };

            var mockParseStrategiesProvider = new Mock<IParseStrategiesProvider>();
            mockParseStrategiesProvider.Setup(m => m.Strategies).Returns(() => mockListOfIParseHtmlStartegy);

            var mockHttpMessageHandler = new MockHttpMessageHandler();
            mockHttpMessageHandler.When(@"http://testUrl").Respond("text/html", "<ul id=\"ListViewInner\"/>");

            var parseStrategiesProvider = mockParseStrategiesProvider.Object;
            var productsHtmlParser = CreateProductsHtmlParser(parseStrategiesProvider, mockHttpMessageHandler);

            var url = @"http://testUrl";

            // Act
            var success = productsHtmlParser.TryParsePage(url, out var result);

            // Assert
            Assert.False(success);
            Assert.Null(result);
        }

        [Fact]
        public void TryParsePage_StrategyMatch_ReturnTrueResultNotNull ()
        {
            // Arrange

            var htmlNode = HtmlNode.CreateNode("<div><div/>"); //htmlNode is out argument for TryParse
            var mockParseHtmlStrategy = new Mock<IParseHtmlStrategy>();
            mockParseHtmlStrategy.Setup(m => m.TryCompute(It.IsAny<HtmlDocument>(), out htmlNode)).Returns(true);
            var mockListOfIParseHtmlStrategy = new List<IParseHtmlStrategy>() { mockParseHtmlStrategy.Object };

            var mockParseStrategiesProvider = new Mock<IParseStrategiesProvider>();
            mockParseStrategiesProvider.Setup(m => m.Strategies).Returns(mockListOfIParseHtmlStrategy);

            var mockHttpMessageHandler = new MockHttpMessageHandler();
            mockHttpMessageHandler.When(@"http://testUrl").Respond("text/html", "<ul id=\"whatever\"/>");

            var parseStrategiesProvider = mockParseStrategiesProvider.Object;
            var productsHtmlParser = CreateProductsHtmlParser(parseStrategiesProvider, mockHttpMessageHandler);

            var url = @"http://testUrl";

            // Act
            var success = productsHtmlParser.TryParsePage(url, out var result);

            // Assert
            Assert.True(success);
            Assert.NotNull(result);
        }
    }
}