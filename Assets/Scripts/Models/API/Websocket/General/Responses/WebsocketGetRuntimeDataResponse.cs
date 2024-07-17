using System;
using System.Collections.Generic;

namespace General
{
    [Serializable]
    public class WebsocketGetRuntimeDataResponse : WebsocketGetBalanceResponse
    {
        public List<WebsocketGetOrderResponse> orders;
        public List<WebsocketGetQuickOrderResponse> quickOrders;
        public List<WebsocketGetTradingBotResponse> tradingBots;
    }
    [Serializable]
    public class WebsocketGetOrderResponse
    {
        public string id;
        public OrderStatusEnum status;
        public bool statusError;
        public string tradingBotId;
        public string symbol;
        public CalculateMargin marginCalculator;
        public OrderTypeEnum orderType;
        public float quantityFilled;
        public float averagePriceFilled;
        public float actualTakeProfitPrice;
        public float paidFundingAmount;
        public List<WebsocketGetThrottleOrderResponse> throttleOrders;
        public long spawnTime; // TIMESTAMP
        public ExitOrderTypeEnum exitOrderType;
    }
    [Serializable]
    public class WebsocketGetThrottleOrderResponse
    {
        public string id;
        public CalculateThrottle throttleCalculator;
        public OrderTypeEnum orderType;
        public TakeProfitTypeEnum breakEvenType;
        public OrderStatusEnum status;
        public bool statusError;
    }
    [Serializable]
    public class WebsocketGetQuickOrderResponse
    {
        public string id;
        public float entryPrice;
        public bool isLong;
        public Preference setting;
    }
    [Serializable]
    public class WebsocketGetTradingBotResponse
    {
        public string id;
        public Preference setting;
    }
}