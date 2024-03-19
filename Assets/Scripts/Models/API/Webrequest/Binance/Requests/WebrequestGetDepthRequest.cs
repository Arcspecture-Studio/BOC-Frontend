using System;

namespace Binance
{
    [Serializable]
    public class WebrequestGetDepthRequest : WebrequestRequest
    {
        public WebrequestGetDepthRequest(bool testnet, string symbol, long limit) : base(testnet)
        {
            path = "/fapi/v1/depth";
            requestType = WebrequestRequestTypeEnum.GET;
            queries = "symbol=" + symbol + "&limit=" + limit;
        }
    }
}