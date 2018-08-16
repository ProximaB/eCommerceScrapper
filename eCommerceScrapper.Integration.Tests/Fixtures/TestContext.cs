using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http.Features;
using WebHostBuilder = Microsoft.AspNetCore.Hosting.WebHostBuilder;

namespace eCommerceScrapper.Integration.Tests.Fixtures
{
    public class TestContext : IDisposable
    {
        private TestServer _server;
        public HttpClient Client { get; private set; }

        public TestContext ()
        {
            SetUpClient();
        }

        private void SetUpClient ()
        {
            var webHostBuilder = new WebHostBuilder();
            var featureCollection = new FeatureCollection();
            _server = new TestServer(webHostBuilder, featureCollection);

            Client = _server.CreateClient();
        }

        public void Dispose ()
        {
            _server?.Dispose();
            Client?.Dispose();
        }
    }
}