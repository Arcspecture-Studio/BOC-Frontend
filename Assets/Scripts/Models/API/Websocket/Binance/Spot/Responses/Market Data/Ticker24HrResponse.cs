using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class Ticker24HrResponse : ResponseWrapper<List<Ticker24HrResult>>
    {
    }

    [Serializable]
    public class Ticker24HrResult
    {
        public string symbol;
        public string priceChange;
        public string priceChangePercent;
        public string weightedAvgPrice;
        public string prevClosePrice;
        public string lastPrice;
        public string lastQty;
        public string bidPrice;
        public string bidQty;
        public string askPrice;
        public string askQty;
        public string openPrice;
        public string highPrice;
        public string lowPrice;
        public string volume;
        public string quoteVolume;
        public long openTime;
        public long closeTime;
        public long firstId;
        public long lastId;
        public long count;
    }
}