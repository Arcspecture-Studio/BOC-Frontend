using System;

namespace Binance
{
    [Serializable]
    public class WebrequestFeeRequest : WebrequestRequest
    {
        public WebrequestFeeRequest(bool testnet, string apiSecret, string symbol) : base(testnet)
        {
            path = "/fapi/v1/commissionRate";
            requestType = WebrequestRequestTypeEnum.GET;

            string queries =
                "symbol=" + symbol;
            UpdateUri(queries, apiSecret);
        }
    }
}