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
        public TakeProfitTypeEnum takeProfitType;
        public double quantityFilled;
        public double averagePriceFilled;
        public double actualTakeProfitPrice;
        public double paidFundingAmount;
        public List<WebsocketGetThrottleOrderResponse> throttleOrders;
    }
    [Serializable]
    public class WebsocketGetThrottleOrderResponse
    {
        public string id;
        public CalculateThrottle throttleCalculator;
        public OrderTypeEnum orderType;
        public OrderStatusEnum status;
        public bool statusError;
    }
    [Serializable]
    public class WebsocketGetQuickOrderResponse
    {
        public string id;
        public string symbol;
        public bool isLong;
        public double entryPrice;
        public TimeframeEnum atrTimeframe;
    }
    [Serializable]
    public class WebsocketGetTradingBotResponse
    {
        public string id;
        public TradingBotSetting botSetting;
        public QuickOrderSetting quickOrderSetting;
    }
}