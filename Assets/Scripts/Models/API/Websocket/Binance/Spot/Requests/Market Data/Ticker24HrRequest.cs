#pragma warning disable CS8632

using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class Ticker24HrRequest : RequestWrapper<Ticker24HrParams>
    {
        public Ticker24HrRequest(string method, Ticker24HrParams param) : base(method, param)
        {
        }
    }

    [Serializable]
    public class Ticker24HrParams
    {
        public List<string> symbols;
        public string? type;

        public Ticker24HrParams(List<string> symbols, string? type)
        {
            this.symbols = symbols;
            this.type = type;
        }
    }
}