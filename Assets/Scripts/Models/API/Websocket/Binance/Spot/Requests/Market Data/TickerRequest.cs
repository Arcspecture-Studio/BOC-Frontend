#pragma warning disable CS8632

using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class TickerRequest : RequestWrapper<TickerParams>
    {
        public TickerRequest(string method, TickerParams param) : base(method, param)
        {
        }
    }

    [Serializable]
    public class TickerParams : Ticker24HrParams
    {
        public string? windowSize;

        public TickerParams(List<string> symbols, string? type, string? windowSize) : base(symbols, type)
        {
            this.windowSize = windowSize;
        }
    }
}