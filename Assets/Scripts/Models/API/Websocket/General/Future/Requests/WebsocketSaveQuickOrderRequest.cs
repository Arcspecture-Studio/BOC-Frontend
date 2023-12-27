using System;

namespace General
{
    [Serializable]
    public class WebsocketSaveQuickOrderRequest : WebsocketGeneralRequest
    {
        public WebsocketOrderIdRequest orderRequest;
        public WebsocketSaveOrderEnum actionToTake;

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
        TimeframeEnum atrTimeframe,
        int atrLength,
        double atrMultiplier,
        bool isLong) : base(WebsocketEventTypeEnum.SAVE_QUICK_ORDER, platform)
        {
            actionToTake = WebsocketSaveOrderEnum.SAVE;
            orderRequest = new WebsocketQuickOrderRequest(symbol, maxLossPercentage, maxLossAmount, weightedQuantity, quantityWeight, takeProfitType,
            riskRewardRatio, takeProfitTrailingCallbackPercentage, entryPrice, entryTimes, atrTimeframe, atrLength, atrMultiplier, isLong);
        }

        public WebsocketSaveQuickOrderRequest(PlatformEnum platform, string orderId) : base(WebsocketEventTypeEnum.SAVE_QUICK_ORDER, platform)
        {
            actionToTake = WebsocketSaveOrderEnum.DELETE;
            orderRequest = new WebsocketOrderIdRequest(orderId);
        }
    }
    [Serializable]
    public class WebsocketQuickOrderRequest : WebsocketOrderIdRequest
    {
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
        TimeframeEnum atrTimeframe,
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
            this.atrTimeframe = atrTimeframe;
            this.atrLength = atrLength;
            this.atrMultiplier = atrMultiplier;
            this.isLong = isLong;
        }
    }
}