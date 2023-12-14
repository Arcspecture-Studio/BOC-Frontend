using System;

namespace Binance
{
    [Serializable]
    public class WebrequestChangeLeverageRequest : WebrequestRequest
    {
        public WebrequestChangeLeverageRequest(bool testnet, string apiSecret, string symbol, long leverage) : base(testnet)
        {
            path = "/fapi/v1/leverage";
            requestType = WebrequestRequestTypeEnum.POST;

            string queries =
                "symbol=" + symbol +
                "&leverage=" + leverage.ToString();
            UpdateUri(queries, apiSecret);
        }
    }
}