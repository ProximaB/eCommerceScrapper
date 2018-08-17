using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using System.Web.Http;

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
            //var webHostBuilder = new WebHostBuilder().ConfigureAppConfiguration()
            //var featureCollection = new FeatureCollection();
            //_server = new TestServer(webHostBuilder, featureCollection);
            //Client = _server.CreateClient();

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            var server = new HttpServer(config);
            var client = new HttpClient(server);

            Client = client;
        }

        public void Dispose ()
        {
            _server?.Dispose();
            Client?.Dispose();
        }

        public class ProductController : ApiController
        {
            [HttpGet]
            [Route("api/product/hello/")]
            public IHttpActionResult Hello ()
            {
                return Ok();
            }
        }
    }
}