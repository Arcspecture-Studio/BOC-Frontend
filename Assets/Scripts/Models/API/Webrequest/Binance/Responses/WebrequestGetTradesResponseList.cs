using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebrequestGetTradesResponseList : List<WebrequestGetTradesResponseData>
    {
    }

    [Serializable]
    public class WebrequestGetTradesResponseData : WebrequestGeneralResponse
    {
        public bool buyer;
        public string commission;
        public string commissionAsset;
        public long id;
        public bool maker;
        public long orderId;
        public string price;
        public string qty;
        public string quoteQty;
        public string realizedPnl;
        public string side;
        public string positionSide;
        public string symbol;
        public long time;
        public string marginAsset;
    }
}