using System;

namespace Binance
{
    [Serializable]
    public class TradesHistoricalRequest : RequestWrapper<TradesHistoricalParams>
    {
        public TradesHistoricalRequest(string method, TradesHistoricalParams param) : base(method, param)
        {
        }
    }

    [Serializable]
    public class TradesHistoricalParams
    {
        public string symbol;
        public long? fromId;
        public long? limit;
        public string apiKey;

        public TradesHistoricalParams(string symbol, long? fromId, long? limit, string apiKey)
        {
            this.symbol = symbol;
            this.fromId = fromId;
            this.limit = limit;
            this.apiKey = apiKey;
        }
    }
}