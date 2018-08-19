using eCommerceScrapper.Interfaces;
using HtmlAgilityPack;
using Moq;
using RichardSzalay.MockHttp;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;
using Xunit.Abstractions;

namespace eCommerceScrapper.Tests
{
    public class StrategiesProcessorBaseTests
    {
        private const string URL_TEST = "http://test.request/";

        private readonly ITestOutputHelper output;

        public StrategiesProcessorBaseTests (ITestOutputHelper output)
        {
            this.output = output;
        }

        private Mock<IParseStrategy> CreateMockIParseStrategy (HtmlNode returnNode)
        {
            var mockParseStrategy = new Mock<IParseStrategy>();
            mockParseStrategy.Setup(m => m.Parser(It.IsAny<HtmlDocument>())).Returns(returnNode);
            return mockParseStrategy;
        }

        [Fact]
        public void Process_SecondStrategyMatch_ResultNotNull ()
        {
            // Arrange
            var mockHttpMessageHandler = new MockHttpMessageHandler();
            mockHttpMessageHandler
                .When(HttpMethod.Get, URL_TEST).Respond("text/html", "Some Respond");

            var mockParseStrategiesProvider = new Mock<IParseStrategiesProvider<IParseStrategy>>();
            mockParseStrategiesProvider.Setup(m => m.Strategies).Returns(new List<IParseStrategy>
            {
                CreateMockIParseStrategy( returnNode: (HtmlNode) null).Object,
                CreateMockIParseStrategy( returnNode: HtmlNode.CreateNode("<div></div>")).Object //Does need to be real node otherwise it will return null
            });
            var strategiesProcessor = new StrategiesProcessorBase<IParseStrategy>(mockParseStrategiesProvider.Object, mockHttpMessageHandler.ToHttpClient());
            // Act
            var result = strategiesProcessor.Process(URL_TEST);
            // Assert
            output.WriteLine($"Name of HtmlNode: {result?.Name ?? "NULL"}");
            Assert.NotNull(result);
        }

        [Fact]
        public void Process_ReturnResultOfFirstMatchedStrategy_ResultMatchFirstStrategy ()
        {
            // Arrange
            var contentOfFirstNode = "First_Strategy";
            var mockHttpMessageHandler = new MockHttpMessageHandler();
            mockHttpMessageHandler
                .When(HttpMethod.Get, URL_TEST).Respond("text/html", "Some Respond");

            var mockParseStrategiesProvider = new Mock<IParseStrategiesProvider<IParseStrategy>>();
            mockParseStrategiesProvider.Setup(m => m.Strategies).Returns(new List<IParseStrategy>
            {
                CreateMockIParseStrategy( returnNode: HtmlNode.CreateNode($"<div>{contentOfFirstNode}</div>")).Object,
                CreateMockIParseStrategy( returnNode: HtmlNode.CreateNode("<div>Second_Strategy</div>")).Object
            });
            var strategiesProcessor = new StrategiesProcessorBase<IParseStrategy>(mockParseStrategiesProvider.Object, mockHttpMessageHandler.ToHttpClient());
            // Act
            var result = strategiesProcessor.Process(URL_TEST);
            var innerText = result.InnerText;
            // Assert
            output.WriteLine($"Content of HtmlNode: {result?.InnerText ?? "NULL"}");
            Assert.Equal(innerText, contentOfFirstNode);
        }

        [Fact]
        public void Process_StrategiesDoNotMatch_ResultNull ()
        {
            // Arrange
            var mockHttpMessageHandler = new MockHttpMessageHandler();
            mockHttpMessageHandler
                .When(HttpMethod.Get, URL_TEST).Respond("text/html", "Some Respond");

            var mockParseStrategiesProvider = new Mock<IParseStrategiesProvider<IParseStrategy>>();
            mockParseStrategiesProvider.Setup(m => m.Strategies).Returns(new List<IParseStrategy>
            {
                CreateMockIParseStrategy( returnNode: (HtmlNode) null).Object,
                CreateMockIParseStrategy( returnNode: (HtmlNode) null).Object,
            });
            // Act
            var strategiesProcessor = new StrategiesProcessorBase<IParseStrategy>(mockParseStrategiesProvider.Object, mockHttpMessageHandler.ToHttpClient());
            var result = strategiesProcessor.Process(URL_TEST);
            // Assert
            output.WriteLine($"Name of HtmlNode: {result?.Name ?? "NULL"}");
            Assert.Null(result);
        }
    }
}