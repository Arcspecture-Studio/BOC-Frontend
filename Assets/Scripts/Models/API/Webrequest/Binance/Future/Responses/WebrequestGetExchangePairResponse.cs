using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebrequestGetExchangePairResponse : WebrequestGeneralResponse
    {
        public List<WebrequestGetExchangePairResponseData> notionalLimits;
    }

    [Serializable]
    public class WebrequestGetExchangePairResponseData
    {
        public string symbol;
        public long notionalLimit;
    }
}