using System;

namespace Binance
{
    [Serializable]
    public class WebrequestQueryOrderRequest : WebrequestRequest
    {
        public WebrequestQueryOrderRequest(bool testnet, string apiSecret, string symbol, long orderId) : base(testnet)
        {
            path = "/fapi/v1/order";
            requestType = WebrequestRequestTypeEnum.GET;

            string queries =
                "symbol=" + symbol +
                "&orderId=" + orderId.ToString();
            UpdateUri(queries, apiSecret);
        }
    }
}