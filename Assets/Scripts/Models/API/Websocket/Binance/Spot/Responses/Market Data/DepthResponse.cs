using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class DepthResponse : ResponseWrapper<DepthResult>
    {
    }

    [Serializable]
    public class DepthResult
    {
        public long lastUpdateId;
        public List<List<string>> bids;
        public List<List<string>> asks;
    }
}