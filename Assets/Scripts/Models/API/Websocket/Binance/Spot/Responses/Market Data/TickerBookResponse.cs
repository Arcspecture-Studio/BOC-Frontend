using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class TickerBookResponse : ResponseWrapper<List<TickerBookResult>>
    {
    }

    [Serializable]
    public class TickerBookResult
    {
        public string symbol;
        public string bidPrice;
        public string bidQty;
        public string askPrice;
        public string askQty;
    }
}