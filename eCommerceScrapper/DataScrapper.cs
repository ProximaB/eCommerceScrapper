//GOTO: Processors.StrategyProcessorBase

//using eCommerceScrapper;
//using eCommerceScrapper.Interfaces;
//using HtmlAgilityPack;

//namespace eCommerceScrapper
//{
//    public partial class DataScrapper<T> where T : IParseStrategy
//    {
//        private readonly IStrategiesProcessor _strategiesProcessor;

//        /*TODO: Make HtmlParser with generic type out if possible, returned type should be provided by concrete strategy*/
//        /*TODO: Dla ebay strategy model(output) zawsze powinien byc taki sam. */
//        /* We can use generic type return public T foo<T>() */

//        public DataScrapper (IStrategiesProcessor strategiesProcessor)
//        {
//            _strategiesProcessor = strategiesProcessor;
//        }

//        public HtmlNode Parse(url);
//    }
//}