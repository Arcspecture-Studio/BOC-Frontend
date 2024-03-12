using System;

namespace Binance
{
    [Serializable]
    public class WebrequestQueryAllOrdersRequest : WebrequestRequest
    {
        public WebrequestQueryAllOrdersRequest(bool testnet, string apiSecret, string symbol, long? orderId, long? startTime, long? endTime, long? limit) : base(testnet)
        {
            path = "/fapi/v1/allOrders";
            requestType = WebrequestRequestTypeEnum.GET;

            string orderIdQuery;
            string startTimeQuery;
            string endTimeQuery;
            string limitQuery;
            if (!orderId.HasValue) orderIdQuery = "";
            else orderIdQuery = "&orderId=" + orderId.ToString();
            if (!startTime.HasValue) startTimeQuery = "";
            else startTimeQuery = "&startTime=" + startTime.ToString();
            if (!endTime.HasValue) endTimeQuery = "";
            else endTimeQuery = "&endTime=" + endTime.ToString();
            if (!limit.HasValue) limitQuery = "";
            else limitQuery = "&limit=" + limit.ToString();
            string queries =
                "symbol=" + symbol +
                orderIdQuery +
                startTimeQuery +
                endTimeQuery +
                limitQuery;
            UpdateUri(queries, apiSecret);
        }
    }
}