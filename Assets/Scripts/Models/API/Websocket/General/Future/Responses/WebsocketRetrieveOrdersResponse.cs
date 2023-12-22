using System;
using System.Collections.Generic;

namespace General
{
    [Serializable]
    public class WebsocketRetrieveOrdersResponse : WebsocketGeneralResponse
    {
        public Dictionary<string, WebsocketRetrieveOrdersData> orders;
    }
    [Serializable]
    public class WebsocketRetrieveOrdersData
    {
        public OrderStatusEnum status;
        public bool statusError;
        public string symbol;
        public CalculateMargin marginCalculator;
        public OrderTypeEnum orderType;
        public OrderTakeProfitTypeEnum takeProfitType;
        public double quantityFilled;
        public double averagePriceFilled;
        public double actualTakeProfitPrice;
        public double paidFundingAmount;
        public Dictionary<string, WebsocketRetrieveThrottleOrdersData> throttleOrders;
    }
    [Serializable]
    public class WebsocketRetrieveThrottleOrdersData
    {
        public OrderStatusEnum status;
        public bool statusError;
        public CalculateThrottle throttleCalculator;
        public OrderTypeEnum orderType;
    }
}