using eCommerceScrapper.Interfaces;
using HtmlAgilityPack;
using System.Net.Http;

namespace eCommerceScrapper.ParseHtmlStrategies
{
    //Brainstorm: Extend ParseHtmlStrategy for validating throu incoming request, if not don't fire Parse, but go to next Strategy
    //Brainstorm: Let Strategy obtain specyfic User-Agent, so it's always get right html, many startegies ensure that reuslt will be supplied if html views for some user-agent would change.
    //Brainstorm: So if there is validation (like pipeline) we can assume that every UserAgent has only one type of html view, for specyfic url, there fore its no need to grup by user-agen for decrese http request.
    //TODO: ParseHthmlStrategy shuld be injectable by httpclient instance, then supply it with OWN UserAgent (achive by DefaultRequestHeaders.UserAgent), provide Validation method on url that will eventauly tryParse or move to the next strategy.
    //TODO: Wyciągnąć wysyłąnie requesta i walidację do osobnej klasy, która będzie enkapsulować Strategie lub Listę strategii,lub zejsc do HtmlParser.cs, na pewno zrobić interface i swobodnie łączyć strategie dla handlera
    //TODO: Idea.. Handler inicjuje się raz a dostarcza mu się strategie z poziomu HtmlParse.cs, w tes sposob nie musimy dostarczac tak dużo zależnosci do kazdego z obiektów.
    //TODO: Strategia powinna tylko parsowac i validować, nie... strategia powinna parsować i dostosowywać requesta, takie było załozenie,
    //TODO: Zostawić parsowanie i walidację i messagerequestPreAction, ale stworzyć interface dla handlera.
    //TODO: Parser nie zmienia się, nie potrzebuje instancji, handler musi przetworzyć wiele strategii. Zasada Single Responsability Principals
    //Klasa odpowedzialna za przetworzenie requesta i zwrocenie wyniku null, htmlnode (ATM)
    public abstract class ParseHtmlStrategy : IParseHtmlStrategy
    {
        private readonly HttpClient _httpClient;

        protected ParseHtmlStrategy (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected abstract HtmlNode Parser (HtmlDocument htmlDocument);

        protected abstract void PreRequestAction (HttpRequestMessage request);

        protected abstract bool UrlValid (string url);

        public HtmlNode Compute (string url)
        {
            if ( !UrlValid(url) )
                return null;

            var htmlDocument = GetHtmlResponse(url);
            var productListHtml = Parser(htmlDocument);
            return productListHtml;
        }

        protected HtmlDocument GetHtmlResponse (string url)// Action<HttpRequestMessage> preRequestAction)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            PreRequestAction(request);

            var htmlString = _httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);

            return htmlDocument;
        }
    }
}