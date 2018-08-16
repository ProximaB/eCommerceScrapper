using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eCommerceScrapper.Integration.Tests.Fixtures
{
    [CollectionDefinition("SystemCollection")]
    public class Collection : ICollectionFixture<TestContext>
    {

    }
}
