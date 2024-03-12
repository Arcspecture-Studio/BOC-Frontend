using System;

namespace Binance
{
    [Serializable]
    public class WebrequestAutoCancelAllOrdersRequest : WebrequestRequest
    {
        public WebrequestAutoCancelAllOrdersRequest(bool testnet, string apiSecret, string symbol, long countdownTime) : base(testnet)
        {
            path = "/fapi/v1/countdownCancelAll";
            requestType = WebrequestRequestTypeEnum.POST;

            string queries =
                "symbol=" + symbol +
                "&countdownTime=" + countdownTime.ToString();
            UpdateUri(queries, apiSecret);
        }
    }
}