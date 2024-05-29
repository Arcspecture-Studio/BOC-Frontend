using System;

namespace Binance
{
    [Serializable]
    public class WebrequestGetDepthRequest : WebrequestRequest
    {
        public WebrequestGetDepthRequest(string platformId, string symbol, int limit) : base(platformId)
        {
            path = "/fapi/v1/depth";
            requestType = WebrequestRequestTypeEnum.GET;
            queries = "symbol=" + symbol + "&limit=" + limit;
        }
    }
}