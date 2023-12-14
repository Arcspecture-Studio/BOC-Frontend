using System;

namespace Binance
{
    [Serializable]
    public class TradesAggregateRequest : RequestWrapper<TradesAggregateParams>
    {
        public TradesAggregateRequest(string method, TradesAggregateParams param) : base(method, param)
        {
        }
    }

    [Serializable]
    public class TradesAggregateParams
    {
        public string symbol;
        public long? fromId;
        public long? startTime;
        public long? endTime;
        public long? limit;

        public TradesAggregateParams(string symbol, long? fromId, long? startTime, long? endTime, long? limit)
        {
            this.symbol = symbol;
            this.fromId = fromId;
            if (startTime != null) this.startTime = startTime;
            if (endTime != null) this.endTime = endTime;
            this.limit = limit;
        }
    }
}