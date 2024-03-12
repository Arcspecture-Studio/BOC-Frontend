using System;

namespace Binance
{
    [Serializable]
    public class WebrequestGetPositionMarginHistoryRequest : WebrequestRequest
    {
        public WebrequestGetPositionMarginHistoryRequest(bool testnet, string apiSecret, string symbol, long? type, long? startTime, long? endTime, long? limit) : base(testnet)
        {
            path = "/fapi/v1/positionMargin/history";
            requestType = WebrequestRequestTypeEnum.GET;

            string typeQuery = !type.HasValue ? "" : ("&type=" + type.ToString());
            string startTimeQuery = !startTime.HasValue ? "" : ("&startTime=" + startTime.ToString());
            string endTimeQuery = !endTime.HasValue ? "" : ("&endTime=" + endTime.ToString());
            string limitQuery = !limit.HasValue ? "" : ("&limit=" + limit.ToString());

            string queries =
                "symbol=" + symbol + typeQuery + startTimeQuery + endTimeQuery + limitQuery;
            UpdateUri(queries, apiSecret);
        }
    }
}