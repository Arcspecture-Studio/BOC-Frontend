using System;

namespace Binance
{
    [Serializable]
    public class KlinesRequest : RequestWrapper<KlinesParams>
    {
        public KlinesRequest(string method, KlinesParams param) : base(method, param)
        {
        }
    }

    [Serializable]
    public class KlinesParams
    {
        public string symbol;
        public string interval;
        public long? startTime;
        public long? endTime;
        public long? limit;

        public KlinesParams(string symbol, string interval, long? startTime, long? endTime, long? limit)
        {
            this.symbol = symbol;
            this.interval = interval;
            this.startTime = startTime;
            this.endTime = endTime;
            this.limit = limit;
        }
    }
}