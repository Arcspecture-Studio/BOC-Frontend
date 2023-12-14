using System;

namespace Binance
{
    [Serializable]
    public class AvgPriceRequest : RequestWrapper<AvgPriceParams>
    {
        public AvgPriceRequest(string method, AvgPriceParams param) : base(method, param)
        {
        }
    }

    [Serializable]
    public class AvgPriceParams
    {
        public string symbol;

        public AvgPriceParams(string symbol)
        {
            this.symbol = symbol;
        }
    }
}