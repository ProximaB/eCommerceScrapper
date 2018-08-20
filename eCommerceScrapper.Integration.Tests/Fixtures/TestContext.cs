using System;
using System.Net.Http;

namespace eCommerceScrapper.Integration.Tests.Fixtures
{
    /// <summary>
    /// The test context of integration tests.
    /// </summary>
    public class TestContext : IDisposable
    {
        public TestContext ()
        {
            this.SetUpClient();
        }

        /// <summary>
        /// Gets the http client.
        /// </summary>
        public HttpClient HttpClient { get; private set; }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose ()
        {
            this.HttpClient?.Dispose();
        }

        /// <summary>
        /// The set up client.
        /// </summary>
        private void SetUpClient ()
        {
            if ( this.HttpClient != null )
            {
                return;
            }

            this.HttpClient = new HttpClient();
        }
    }
}