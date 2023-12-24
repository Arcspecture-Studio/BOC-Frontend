using System;

namespace General
{
    [Serializable]
    public class WebsocketSaveQuickOrderRequest : WebsocketGeneralRequest
    {
        public WebsocketQuickOrderRequest orderRequest;

        public WebsocketSaveQuickOrderRequest(WebsocketEventTypeEnum eventType, PlatformEnum platform) : base(eventType, platform)
        {
        }
    }
    [Serializable]
    public class WebsocketQuickOrderRequest
    {
        public string orderId;
        public string symbol;
        public double maxLossPercentage;
        public double maxLossAmount;
        public bool weightedQuantity;
        public double quantityWeight;
        public OrderTakeProfitTypeEnum takeProfitType;
        public double riskRewardRatio;
        public double takeProfitTrailingCallbackPercentage;
        public double entryPrice; // -1 means null
        public int entryTimes;
        public TimeframeEnum atrTimeframe;
        public int atrLength;
        public double atrMultiplier;
    }
}