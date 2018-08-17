using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eCommerceScrapper.Extensions
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> GetAsync
            (this HttpClient httpClient, string uri, Action<HttpRequestMessage> preAction)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            preAction(httpRequestMessage);

            return httpClient.SendAsync(httpRequestMessage);
        }

        public static Task<HttpResponseMessage> PostAsJsonAsync<T>
            (this HttpClient httpClient, string uri, T value, Action<HttpRequestMessage> preAction)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new ObjectContent<T>
                    (value, new JsonMediaTypeFormatter(), (MediaTypeHeaderValue) null)
            };
            preAction(httpRequestMessage);

            return httpClient.SendAsync(httpRequestMessage);
        }

        public static string GetHtmlPreAction
            (this HttpClient httpClient, string uri, Action<HttpRequestMessage> preAction) //temp workaround
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            preAction(httpRequestMessage);

            return httpClient.SendAsync(httpRequestMessage).Result.Content.ReadAsStringAsync().Result;
        }
    }
}