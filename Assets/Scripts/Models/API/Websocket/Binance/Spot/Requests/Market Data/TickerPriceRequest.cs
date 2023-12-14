using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class TickerPriceRequest : RequestWrapper<TickerPriceParams>
    {
        public TickerPriceRequest(string method, TickerPriceParams param) : base(method, param)
        {
        }
    }

    [Serializable]
    public class TickerPriceParams
    {
        public List<string> symbols;

        public TickerPriceParams(List<string> symbols)
        {
            this.symbols = symbols;
        }
    }
}