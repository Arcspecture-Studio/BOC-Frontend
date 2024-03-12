using System;

namespace Binance
{
    [Serializable]
    public class WebrequestGetExchangePairRequest : WebrequestRequest
    {
        public WebrequestGetExchangePairRequest(bool testnet) : base(testnet)
        {
            path = "/fapi/v1/pmExchangeInfo";
            requestType = WebrequestRequestTypeEnum.GET;

            uri = host + path;
        }
    }
}