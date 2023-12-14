using System;

namespace Binance
{
    [Serializable]
    public class TradesRecentRequest : RequestWrapper<TradesRecentParams>
    {
        public TradesRecentRequest(string method, TradesRecentParams param) : base(method, param)
        {
        }
    }

    [Serializable]
    public class TradesRecentParams : DepthParams
    {
        public TradesRecentParams(string symbol, long? limit) : base(symbol, limit)
        {
        }
    }
}