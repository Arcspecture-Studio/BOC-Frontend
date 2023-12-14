public static class WebsocketStreamNameEnum
{
    public const string tradeAggregate = "@aggTrade";           // <symbol>@aggTrade
    public const string markPrice = "@markPrice";               // <symbol>@markPrice
    public const string markPrice1s = "@markPrice@1s";          // <symbol>@markPrice@1s
    public const string markPriceAll = "!markPrice@arr";        // !markPrice@arr
    public const string markPriceAll1s = "!markPrice@arr@1s";   // !markPrice@arr@1s
    public const string kline = "@kline_";                      // <symbol>@kline_<interval>
    public const string klineContinous = "@continuousKline_";   // <symbol>_<contractType>@continuousKline_<interval>
    public const string tickerMini = "@miniTicker";             // <symbol>@miniTicker
    public const string tickerMiniAll = "!miniTicker@arr";      // !miniTicker@arr
    public const string ticker = "@ticker";                     // <symbol>@ticker
    public const string tickerAll = "!ticker@arr";              // !ticker@arr
    public const string tickerBook = "@bookTicker";             // <symbol>@bookTicker
    public const string tickerBookAll = "!bookTicker";          // !bookTicker
    public const string orderForce = "@forceOrder";             // <symbol>@forceOrder
    public const string orderForceAll = "!forceOrder@arr";      // !forceOrder@arr
    public const string depthPartial5 = "@depth5";              // <symbol>@depth<levels> OR <symbol>@depth<levels>@500ms OR <symbol>@depth<levels>@100ms (default update speed 250ms)
    public const string depthPartial10 = "@depth10";            // <symbol>@depth<levels> OR <symbol>@depth<levels>@500ms OR <symbol>@depth<levels>@100ms (default update speed 250ms)
    public const string depthPartial20 = "@depth20";            // <symbol>@depth<levels> OR <symbol>@depth<levels>@500ms OR <symbol>@depth<levels>@100ms (default update speed 250ms)
    public const string depth = "@depth";                       // <symbol>@depth OR <symbol>@depth@500ms OR <symbol>@depth@100ms (default update speed 250ms)
    public const string indexComposite = "@compositeIndex";     // <symbol>@compositeIndex
    public const string infoContract = "!contractInfo";         // !contractInfo
}