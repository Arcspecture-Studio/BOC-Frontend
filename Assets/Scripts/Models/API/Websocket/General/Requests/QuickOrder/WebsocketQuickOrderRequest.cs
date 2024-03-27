using System;

namespace General
{
    [Serializable]
    public class WebsocketQuickOrderRequest : WebsocketOrderIdRequest
    {
        public PlatformEnum platform;
        public QuickOrderSetting setting;
        public double entryPrice; // -1 means null
        public bool isLong;

        public WebsocketQuickOrderRequest(PlatformEnum platform,
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
        bool isLong) : base(null)
        {
            this.platform = platform;
            this.setting = new(symbol, maxLossPercentage, maxLossAmount, weightedQuantity, quantityWeight, takeProfitType, riskRewardRatio, takeProfitTrailingCallbackPercentage, entryTimes, atrInterval, atrLength, atrMultiplier);
            this.entryPrice = entryPrice;
            this.isLong = isLong;
        }
    }
}