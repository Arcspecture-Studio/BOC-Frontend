using System;

namespace Binance
{
    [Serializable]
    public class WebrequestGetBalanceRequest : WebrequestRequest
    {
        public WebrequestGetBalanceRequest(bool testnet) : base(testnet)
        {
            path = "/fapi/v2/balance";
            requestType = WebrequestRequestTypeEnum.GET;
        }
    }
}