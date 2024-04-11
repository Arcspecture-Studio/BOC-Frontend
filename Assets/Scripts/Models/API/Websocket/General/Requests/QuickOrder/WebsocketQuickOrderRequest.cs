using System;

namespace General
{
    [Serializable]
    public class WebsocketQuickOrderRequest
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
            OrderTypeEnum orderType,
            double riskRewardRatio,
            double takeProfitTrailingCallbackPercentage,
            double entryPrice,
            int entryTimes,
            TimeframeEnum atrTimeframe,
            int atrLength,
            double atrMultiplier,
            bool isLong)
        {
            this.platform = platform;
            this.setting = new(symbol, maxLossPercentage, maxLossAmount, weightedQuantity, quantityWeight, takeProfitType, orderType,
            riskRewardRatio, takeProfitTrailingCallbackPercentage, entryTimes, atrTimeframe, atrLength, atrMultiplier);
            this.entryPrice = entryPrice;
            this.isLong = isLong;
        }
    }
}