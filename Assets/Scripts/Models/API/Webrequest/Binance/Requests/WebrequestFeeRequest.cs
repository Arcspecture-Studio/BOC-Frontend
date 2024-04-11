using System;

namespace Binance
{
    [Serializable]
    public class WebrequestFeeRequest : WebrequestRequest
    {
        public WebrequestFeeRequest(bool testnet, string symbol) : base(testnet)
        {
            path = "/fapi/v1/commissionRate";
            requestType = WebrequestRequestTypeEnum.GET;
            queries = "symbol=" + symbol;
        }
    }
}