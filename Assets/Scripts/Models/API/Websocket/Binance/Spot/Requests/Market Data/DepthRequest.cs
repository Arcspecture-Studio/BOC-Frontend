using System;

namespace Binance
{
    [Serializable]
    public class DepthRequest : RequestWrapper<DepthParams>
    {
        public DepthRequest(string method, DepthParams param) : base(method, param)
        {
        }
    }

    [Serializable]
    public class DepthParams
    {
        public string symbol;
        public long? limit;

        public DepthParams(string symbol, long? limit)
        {
            this.symbol = symbol;
            this.limit = limit;
        }
    }
}