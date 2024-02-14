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
        TakeProfitTypeEnum takeProfitType,
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
            orderRequest = new WebsocketQuickOrderRequest(symbol, maxLossPercentage, maxLossAmount, weightedQuantity, quantityWeight, takeProfitType, riskRewardRatio, takeProfitTrailingCallbackPercentage, entryPrice, entryTimes, atrInterval, atrLength, atrMultiplier, isLong);
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
        public QuickOrderSetting setting;
        public double entryPrice; // -1 means null
        public bool isLong;

        public WebsocketQuickOrderRequest(string symbol,
        double maxLossPercentage,
        double maxLossAmount,
        bool weightedQuantity,
        double quantityWeight,
        TakeProfitTypeEnum takeProfitType,
        double riskRewardRatio,
        double takeProfitTrailingCallbackPercentage,
        double entryPrice,
        int entryTimes,
        string atrInterval,
        int atrLength,
        double atrMultiplier,
        bool isLong) : base(null)
        {
            setting = new(symbol, maxLossPercentage, maxLossAmount, weightedQuantity, quantityWeight, takeProfitType, riskRewardRatio, takeProfitTrailingCallbackPercentage, entryTimes, atrInterval, atrLength, atrMultiplier);
            this.entryPrice = entryPrice;
            this.isLong = isLong;
        }
    }
}