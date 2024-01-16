using System;

namespace General
{
    [Serializable]
    public class WebsocketSaveQuickOrderRequest : WebsocketGeneralRequest
    {
        public WebsocketOrderIdRequest orderRequest;
        public WebsocketDataActionEnum actionToTake;

        public WebsocketSaveQuickOrderRequest(PlatformEnum platform,
        string symbol,
        double maxLossPercentage,
        double maxLossAmount,
        bool weightedQuantity,
        double quantityWeight,
        OrderTakeProfitTypeEnum takeProfitType,
        double riskRewardRatio,
        double takeProfitTrailingCallbackPercentage,
        double entryPrice,
        int entryTimes,
        string atrInterval,
        int atrLength,
        double atrMultiplier,
        bool isLong) : base(WebsocketEventTypeEnum.SAVE_QUICK_ORDER, platform)
        {
            actionToTake = WebsocketDataActionEnum.SAVE;
            orderRequest = new WebsocketQuickOrderRequest(symbol, maxLossPercentage, maxLossAmount, weightedQuantity, quantityWeight, takeProfitType,
            riskRewardRatio, takeProfitTrailingCallbackPercentage, entryPrice, entryTimes, atrInterval, atrLength, atrMultiplier, isLong);
        }

        public WebsocketSaveQuickOrderRequest(PlatformEnum platform, string orderId) : base(WebsocketEventTypeEnum.SAVE_QUICK_ORDER, platform)
        {
            actionToTake = WebsocketDataActionEnum.DELETE;
            orderRequest = new WebsocketOrderIdRequest(orderId);
        }
    }
    [Serializable]
    public class WebsocketQuickOrderRequest : WebsocketOrderIdRequest
    {
        public string symbol;
        public double maxLossPercentage; // 0 means null
        public double maxLossAmount; // 0 means null
        public bool weightedQuantity;
        public double quantityWeight;
        public OrderTakeProfitTypeEnum takeProfitType;
        public double riskRewardRatio;
        public double takeProfitTrailingCallbackPercentage;
        public double entryPrice; // -1 means null
        public int entryTimes;
        public string atrInterval;
        public int atrLength;
        public double atrMultiplier;
        public bool isLong;

        public WebsocketQuickOrderRequest(string symbol,
        double maxLossPercentage,
        double maxLossAmount,
        bool weightedQuantity,
        double quantityWeight,
        OrderTakeProfitTypeEnum takeProfitType,
        double riskRewardRatio,
        double takeProfitTrailingCallbackPercentage,
        double entryPrice,
        int entryTimes,
        string atrInterval,
        int atrLength,
        double atrMultiplier,
        bool isLong) : base(null)
        {
            this.symbol = symbol;
            this.maxLossPercentage = maxLossPercentage;
            this.maxLossAmount = maxLossAmount;
            this.weightedQuantity = weightedQuantity;
            this.quantityWeight = quantityWeight;
            this.takeProfitType = takeProfitType;
            this.riskRewardRatio = riskRewardRatio;
            this.takeProfitTrailingCallbackPercentage = takeProfitTrailingCallbackPercentage;
            this.entryPrice = entryPrice;
            this.entryTimes = entryTimes;
            this.atrInterval = atrInterval;
            this.atrLength = atrLength;
            this.atrMultiplier = atrMultiplier;
            this.isLong = isLong;
        }
    }
}