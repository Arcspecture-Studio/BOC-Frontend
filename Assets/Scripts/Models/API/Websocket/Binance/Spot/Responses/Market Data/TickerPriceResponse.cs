using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class TickerPriceResponse : ResponseWrapper<List<TickerPriceResult>>
    {
    }

    [Serializable]
    public class TickerPriceResult
    {
        public string symbol;
        public string price;
    }
}