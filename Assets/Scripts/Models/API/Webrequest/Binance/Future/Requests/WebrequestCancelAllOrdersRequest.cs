using System;

namespace Binance
{
    [Serializable]
    public class WebrequestCancelAllOrdersRequest : WebrequestRequest
    {
        public WebrequestCancelAllOrdersRequest(bool testnet, string apiSecret, string symbol) : base(testnet)
        {
            path = "/fapi/v1/allOpenOrders";
            requestType = WebrequestRequestTypeEnum.DELETE;

            string queries =
                "symbol=" + symbol;
            UpdateUri(queries, apiSecret);
        }
    }
}