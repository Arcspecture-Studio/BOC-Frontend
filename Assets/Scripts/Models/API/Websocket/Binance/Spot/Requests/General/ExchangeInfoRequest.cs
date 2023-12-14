using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class ExchangeInfoRequest : RequestWrapper<ExchangeInfoParams>
    {
        public ExchangeInfoRequest(string method, ExchangeInfoParams param) : base(method, param)
        {
        }
    }

    [Serializable]
    public class ExchangeInfoParams
    {
        public List<string> symbols;
        //public List<string> permissions;

        public ExchangeInfoParams(List<string> symbols)
        {
            this.symbols = symbols;
            //this.permissions = permissions;
        }
    }
}