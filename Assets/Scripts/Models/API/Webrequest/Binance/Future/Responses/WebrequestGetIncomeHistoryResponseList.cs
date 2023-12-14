using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebrequestGetIncomeHistoryResponseList : List<WebrequestGetIncomeHistoryResponseData>
    {
    }

    [Serializable]
    public class WebrequestGetIncomeHistoryResponseData : WebrequestGeneralResponse
    {
        public string symbol;
        public string incomeType;
        public string income;
        public string asset;
        public string info;
        public long time;
        public string tranId;
        public string tradeId;
    }
}