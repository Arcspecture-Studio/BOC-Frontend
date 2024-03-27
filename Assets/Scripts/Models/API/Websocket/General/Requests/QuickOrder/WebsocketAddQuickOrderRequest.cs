using System;

namespace General
{
    [Serializable]
    public class WebsocketAddQuickOrderRequest : WebsocketGeneralRequest
    {
        public WebsocketQuickOrderRequest orderRequest;

        public WebsocketAddQuickOrderRequest(string token,
            PlatformEnum platform,
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
            bool isLong) : base(WebsocketEventTypeEnum.ADD_QUICK_ORDER, token)
        {
            orderRequest = new(platform, symbol, maxLossPercentage, maxLossAmount, weightedQuantity, quantityWeight, takeProfitType, riskRewardRatio,
            takeProfitTrailingCallbackPercentage, entryPrice, entryTimes, atrInterval, atrLength, atrMultiplier, isLong);
        }
    }
}