using System;

namespace Binance
{
    [Serializable]
    public class WebrequestGetExchangeInfoRequest : WebrequestRequest
    {
        public WebrequestGetExchangeInfoRequest(bool testnet) : base(testnet)
        {
            path = "/fapi/v1/exchangeInfo";
            requestType = WebrequestRequestTypeEnum.GET;

            uri = host + path;
        }
    }
}