using eCommerceScrapper.Interfaces;
using HtmlAgilityPack;
using Moq;
using RichardSzalay.MockHttp;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

#pragma warning disable 219

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

        // ReSharper disable once RedundantAssignment
        private Mock<IParseHtmlStrategy> MockParseHtmlStrategyProvider (HtmlNode htmlNodeOut, bool returnValue)
        {
            var mockInValidParseHtmlStrategy = new Mock<IParseHtmlStrategy>();
            mockInValidParseHtmlStrategy.Setup(m => m.TryCompute(It.IsAny<HtmlDocument>(), out htmlNodeOut)).Returns(returnValue);
            return mockInValidParseHtmlStrategy;
        }

        [Fact]
        public void TryParsePage_StrategiesDontMatch_ReturnFalseResultIsNull ()
        {
            // Arrange
            var mockListOfIParseHtmlStartegy = new List<IParseHtmlStrategy>()
            {
                MockParseHtmlStrategyProvider(htmlNodeOut: (HtmlNode)null, returnValue: false).Object
            };

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
            var mockListOfIParseHtmlStrategy = new List<IParseHtmlStrategy>()
            {
                MockParseHtmlStrategyProvider(htmlNodeOut : HtmlNode.CreateNode("<a></a>"), returnValue: true).Object
            };

            var mockParseStrategiesProvider = new Mock<IParseStrategiesProvider>();
            mockParseStrategiesProvider.Setup(m => m.Strategies).Returns(mockListOfIParseHtmlStrategy);

            var mockHttpMessageHandler = new MockHttpMessageHandler();
            mockHttpMessageHandler.When(@"http://testUrl").Respond("text/html", "<div>node<div/>");

            var parseStrategiesProvider = mockParseStrategiesProvider.Object;
            var productsHtmlParser = CreateProductsHtmlParser(parseStrategiesProvider, mockHttpMessageHandler);

            var url = @"http://testUrl";

            // Act
            var success = productsHtmlParser.TryParsePage(url, out var result);

            // Assert
            Assert.True(success);
            Assert.NotNull(result);
        }

        [Fact]
        [SuppressMessage("ReSharper", "RedundantAssignment")]
        public void TryParsePage_SecondStrategyFormListMatch_ReturnTrueResultNotNull ()
        {
            // Arrange
            var mockListOfIParseHtmlStrategy = new List<IParseHtmlStrategy>()
            {
                MockParseHtmlStrategyProvider(htmlNodeOut : null, returnValue: false).Object,
                MockParseHtmlStrategyProvider(htmlNodeOut : HtmlNode.CreateNode("<div>node<div/>"), returnValue: true).Object,
            };

            var mockParseStrategiesProvider = new Mock<IParseStrategiesProvider>();
            mockParseStrategiesProvider.Setup(m => m.Strategies).Returns(mockListOfIParseHtmlStrategy);

            var mockHttpMessageHandler = new MockHttpMessageHandler();
            mockHttpMessageHandler.When(@"http://testUrl").Respond("text/html", "<div>node<div/>");

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