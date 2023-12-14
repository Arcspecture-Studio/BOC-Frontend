using System;

namespace Binance
{
    [Serializable]
    public class UiKlinesRequest : RequestWrapper<UiKlinesParams>
    {
        public UiKlinesRequest(string method, UiKlinesParams param) : base(method, param)
        {
        }
    }

    [Serializable]
    public class UiKlinesParams : KlinesParams
    {
        public UiKlinesParams(string symbol, string interval, long? startTime, long? endTime, long? limit) : base(symbol, interval, startTime, endTime, limit)
        {
        }
    }
}