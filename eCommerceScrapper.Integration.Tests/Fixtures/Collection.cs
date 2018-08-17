using Xunit;

namespace eCommerceScrapper.Integration.Tests.Fixtures
{
    [CollectionDefinition("SystemCollection")]
    public class Collection : ICollectionFixture<TestContext>
    {
    }
}