using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class TradesRecentResponse : ResponseWrapper<List<TradesRecentResult>>
    {
    }

    [Serializable]
    public class TradesRecentResult
    {
        public long id;
        public string price;
        public string qty;
        public string quoteQty;
        public long time;
        public bool isBuyerMaker;
        public bool isBestMatch;
    }
}