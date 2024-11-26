using System;
using System.Collections.Generic;

namespace General
{
    [Serializable]
    public class WebsocketGetRuntimeDataResponse : WebsocketGetBalanceResponse
    {
        public List<WebsocketGetOrderDataResponse> orders;
        public List<WebsocketGetQuickOrderDataResponse> quickOrders;
        public List<WebsocketGetTradingBotDataResponse> tradingBots;
    }
    [Serializable]
    public class WebsocketGetOrderDataResponse : WebsocketAddOrderDataResponse
    {
        public OrderStatusEnum status;
        public bool statusError;
        public string tradingBotId;
        public string symbol;
        public OrderTypeEnum orderType;
        public float quantityFilled;
        public float averagePriceFilled;
        public float actualTakeProfitPrice;
        public float paidFundingAmount;
        public List<WebsocketGetThrottleOrderDataResponse> throttleOrders;
    }
    [Serializable]
    public class WebsocketGetThrottleOrderDataResponse
    {
        public string id;
        public CalculateThrottle throttleCalculator;
        public OrderTypeEnum orderType;
        public TakeProfitTypeEnum breakEvenType;
        public OrderStatusEnum status;
        public bool statusError;
    }
    [Serializable]
    public class WebsocketGetQuickOrderDataResponse
    {
        public string id;
        public float entryPrice;
        public bool isLong;
        public Preference setting;
    }
    [Serializable]
    public class WebsocketGetTradingBotDataResponse
    {
        public string id;
        public Preference setting;
    }
}