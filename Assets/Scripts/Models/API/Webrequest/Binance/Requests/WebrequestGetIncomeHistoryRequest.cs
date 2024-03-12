#pragma warning disable CS8632

using System;
using WebSocketSharp;

namespace Binance
{
    [Serializable]
    public class WebrequestGetIncomeHistoryRequest : WebrequestRequest
    {
        public WebrequestGetIncomeHistoryRequest(bool testnet, string apiSecret, string symbol, string? incomeType, long? startTime, long? endTime, long? limit) : base(testnet)
        {
            path = "/fapi/v1/income";
            requestType = WebrequestRequestTypeEnum.GET;

            string incomeTypeQuery = incomeType.IsNullOrEmpty() ? "" : ("&incomeType=" + incomeType.ToString());
            string startTimeQuery = !startTime.HasValue ? "" : ("&startTime=" + startTime.ToString());
            string endTimeQuery = !endTime.HasValue ? "" : ("&endTime=" + endTime.ToString());
            string limitQuery = !limit.HasValue ? "" : ("&limit=" + limit.ToString());
            string queries = "symbol=" + symbol + incomeTypeQuery + startTimeQuery + endTimeQuery + limitQuery;
            UpdateUri(queries, apiSecret);
        }
    }
}