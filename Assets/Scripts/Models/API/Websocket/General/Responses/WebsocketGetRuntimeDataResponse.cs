using System;
using System.Collections.Generic;

namespace General
{
    [Serializable]
    public class WebsocketGetRuntimeDataResponse : WebsocketGeneralResponse
    {
        public Dictionary<string, WebsocketGetOrderResponse> orders;
        public Dictionary<string, WebsocketGetQuickOrderResponse> quickOrders;
        public Dictionary<string, WebsocketGetTradingBotResponse> tradingBots;
    }
    [Serializable]
    public class WebsocketGetOrderResponse
    {
        public OrderStatusEnum status;
        public bool statusError;
        public string symbol;
        public CalculateMargin marginCalculator;
        public OrderTypeEnum orderType;
        public TakeProfitTypeEnum takeProfitType;
        public double quantityFilled;
        public double averagePriceFilled;
        public double actualTakeProfitPrice;
        public double paidFundingAmount;
        public Dictionary<string, WebsocketGetThrottleOrderResponse> throttleOrders;
    }
    [Serializable]
    public class WebsocketGetThrottleOrderResponse
    {
        public CalculateThrottle throttleCalculator;
        public OrderTypeEnum orderType;
        public OrderStatusEnum status;
        public bool statusError;
    }
    [Serializable]
    public class WebsocketGetQuickOrderResponse
    {
        public string symbol;
        public bool isLong;
        public double entryPrice;
        public string atrInterval;
    }
    [Serializable]
    public class WebsocketGetTradingBotResponse
    {
        public BotTypeEnum botType;
        public string symbol;
    }
}