using System;

namespace Binance
{
    [Serializable]
    public class WebrequestQueryOpenOrderRequest : WebrequestRequest
    {
        public WebrequestQueryOpenOrderRequest(bool testnet, string apiSecret, string symbol, long orderId) : base(testnet)
        {
            path = "/fapi/v1/openOrder";
            requestType = WebrequestRequestTypeEnum.GET;

            string queries =
                "symbol=" + symbol +
                "&orderId=" + orderId.ToString();
            UpdateUri(queries, apiSecret);
        }
    }
}