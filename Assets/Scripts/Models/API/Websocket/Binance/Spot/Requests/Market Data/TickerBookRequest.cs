using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class TickerBookRequest : RequestWrapper<TickerBookParams>
    {
        public TickerBookRequest(string method, TickerBookParams param) : base(method, param)
        {
        }
    }

    [Serializable]
    public class TickerBookParams : TickerPriceParams
    {
        public TickerBookParams(List<string> symbols) : base(symbols)
        {
        }
    }
}