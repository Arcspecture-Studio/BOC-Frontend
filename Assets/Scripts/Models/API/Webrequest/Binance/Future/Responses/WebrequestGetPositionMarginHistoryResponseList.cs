using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebrequestGetPositionMarginHistoryResponseList : List<WebrequestGetPositionMarginHistoryResponseData>
    {
    }

    [Serializable]
    public class WebrequestGetPositionMarginHistoryResponseData : WebrequestGeneralResponse
    {
        public string symbol;
        public long type;
        public string deltaType;
        public string amount;
        public string asset;
        public long time;
        public string positionSide;
    }
}