using System;

namespace Binance
{
    [Serializable]
    public class AvgPriceResponse : ResponseWrapper<AvgPriceResult>
    {
    }

    [Serializable]
    public class AvgPriceResult
    {
        public long mins;
        public string price;
    }
}