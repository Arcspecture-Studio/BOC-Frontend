using System;

namespace Binance
{
    [Serializable]
    public class WebrequestQueryAllOpenOrdersRequest : WebrequestRequest
    {
        public WebrequestQueryAllOpenOrdersRequest(bool testnet, string apiSecret, string symbol) : base(testnet)
        {
            path = "/fapi/v1/openOrders";
            requestType = WebrequestRequestTypeEnum.GET;

            string queries =
                "symbol=" + symbol;
            UpdateUri(queries, apiSecret);
        }
    }
}