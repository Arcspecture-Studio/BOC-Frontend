using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebrequestGetLeverageBracketResponse : WebrequestGeneralResponse
    {
        public string symbol;
        public List<WebrequestGetLeverageBracketResponseData> brackets;
    }

    [Serializable]
    public class WebrequestGetLeverageBracketResponseData
    {
        public long bracket;
        public long initialLeverage;
        public long notionalCap;
        public long notionalFloor;
        public double maintMarginRatio;
        public long cum;
    }
}