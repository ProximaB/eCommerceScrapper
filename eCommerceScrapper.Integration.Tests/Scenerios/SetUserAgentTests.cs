using eCommerceScrapper.Integration.Tests.Fixtures;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace eCommerceScrapper.Integration.Tests.Scenerios
{
    [Collection("SystemCollection")]
    public class SetUserAgentTests
    {
        public readonly TestContext Context;

        public SetUserAgentTests (TestContext context)
        {
            Context = context;
        }

        [Fact]
        public async Task PingReturnsOkResponse ()
        {
            var response = await Context.Client.GetAsync("/ping");

            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}