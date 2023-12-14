using System;

namespace Binance
{
    [Serializable]
    public class WebrequestGetTradesRequest : WebrequestRequest
    {
        public WebrequestGetTradesRequest(bool testnet, string apiSecret, string symbol, long? startTime, long? endTime, long? fromId, long? limit) : base(testnet)
        {
            path = "/fapi/v1/userTrades";
            requestType = WebrequestRequestTypeEnum.GET;

            string startTimeQuery = !startTime.HasValue ? "" : ("&startTime=" + startTime.ToString());
            string endTimeQuery = !endTime.HasValue ? "" : ("&endTime=" + endTime.ToString());
            string fromIdQuery = !fromId.HasValue ? "" : ("&fromId=" + fromId.ToString());
            string limitQuery = !limit.HasValue ? "" : ("&limit=" + limit.ToString());
            string queries = "symbol=" + symbol + startTimeQuery + endTimeQuery + fromIdQuery + limitQuery;
            UpdateUri(queries, apiSecret);
        }
    }
}