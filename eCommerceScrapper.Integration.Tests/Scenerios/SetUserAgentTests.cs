using eCommerceScrapper.Integration.Tests.Fixtures;
using FluentAssertions;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xunit;

namespace eCommerceScrapper.Integration.Tests.Scenerios
{
    using eCommerceScrapper.Integration.Tests.Models;
    using System.Net.Http;
    using Xunit.Abstractions;

    [Collection("SystemCollection")]
    public class SetUserAgentTests
    {
        private readonly ITestOutputHelper _output;

        public SetUserAgentTests (TestContext context, ITestOutputHelper output)
        {
            Context = context;
            this._output = output;
        }

        public readonly TestContext Context;

        /// <summary>
        /// Setting user agent, by HttpRequestMessage and send request to
        /// https://httpbin.org an online service that returning all passed headers and content.
        /// Comparing respond with previously set User-Agent header confirms us that changing UserAgent works.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        public async Task SettingUserAgent_SetUserAgent_GetResponseWithSameUserAgent ()
        {
            // Arrange
            var httpBinUrl = "https://httpbin.org/get";
            var testUserAgent = "customUserAgent";
            var message = new HttpRequestMessage(HttpMethod.Get, httpBinUrl);
            var result = message.Headers.TryAddWithoutValidation("User-Agent", testUserAgent);

            // Act
            var httpResponseMessage = await this.Context.HttpClient.SendAsync(message);
            var receiveContent = await httpResponseMessage.Content.ReadAsStringAsync();
            this._output.WriteLine($"Received content: {receiveContent}");
            HttpBinRespond respond = JsonConvert.DeserializeObject<HttpBinRespond>(receiveContent);
            var respondedUserAgent = respond.headers.User_Agent;

            //Assert
            this._output.WriteLine($"Output: {respondedUserAgent}");
            result.Should().BeTrue();
            respondedUserAgent.Should().Be(testUserAgent);
        }

        [Fact]
        public async Task SettingUserAgent_EmptyUserAgent_GetResponseWithNoUserAgentProvided ()
        {
            // Arrange
            var httpBinUrl = "https://httpbin.org/get";
            var testUserAgent = string.Empty;
            var message = new HttpRequestMessage(HttpMethod.Get, httpBinUrl);
            var result = message.Headers.TryAddWithoutValidation("User-Agent", testUserAgent);

            // Act
            var httpResponseMessage = await this.Context.HttpClient.SendAsync(message);
            var receiveContent = await httpResponseMessage.Content.ReadAsStringAsync();
            this._output.WriteLine($"Received content: {receiveContent}");
            HttpBinRespond respond = JsonConvert.DeserializeObject<HttpBinRespond>(receiveContent);
            var respondedUserAgent = respond.headers.User_Agent;

            //Assert
            this._output.WriteLine($"Output: {respondedUserAgent}");
            respondedUserAgent.Should().Be(testUserAgent);
        }
    }
}