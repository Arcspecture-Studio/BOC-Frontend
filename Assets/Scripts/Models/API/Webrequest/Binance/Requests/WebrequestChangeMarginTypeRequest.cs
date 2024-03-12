using System;

namespace Binance
{
    [Serializable]
    public class WebrequestChangeMarginTypeRequest : WebrequestRequest
    {
        public WebrequestChangeMarginTypeRequest(bool testnet, string apiSecret, string symbol, string marginType) : base(testnet)
        {
            path = "/fapi/v1/marginType";
            requestType = WebrequestRequestTypeEnum.POST;

            string queries =
                "symbol=" + symbol +
                "&marginType=" + marginType.ToString();
            UpdateUri(queries, apiSecret);
        }
    }
}