using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebrequestGetPositionInfoResponseList : List<WebrequestGetPositionInfoResponseData>
    {
    }

    [Serializable]
    public class WebrequestGetPositionInfoResponseData
    {
        public string symbol;
        public string positionAmt;
        public string entryPrice;
        public string markPrice;
        public string unRealizedProfit;
        public string liquidationPrice;
        public string leverage;
        public string maxNotionalValue;
        public string marginType;
        public string isolatedMargin;
        public string isAutoAddMargin;
        public string positionSide;
        public string notional;
        public string isolatedWalle;
        public string updateTime;
    }
}