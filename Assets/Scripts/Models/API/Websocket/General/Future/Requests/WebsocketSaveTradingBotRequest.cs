using System;

namespace General
{
    [Serializable]
    public class WebsocketSaveTradingBotRequest : WebsocketGeneralRequest
    {
        public WebsocketDataActionEnum actionToTake;
        public BotTypeEnum botType;
        public WebsocketQuickOrderRequest quickOrderSetting;

        public WebsocketSaveTradingBotRequest(PlatformEnum platform, BotTypeEnum botType) : base(WebsocketEventTypeEnum.SAVE_TRADING_BOT, platform)
        {
            actionToTake = WebsocketDataActionEnum.DELETE;
            this.botType = botType;
        }
        public WebsocketSaveTradingBotRequest(PlatformEnum platform, BotTypeEnum botType,
        string symbol,
        double maxLossPercentage,
        double maxLossAmount,
        bool weightedQuantity,
        double quantityWeight,
        OrderTakeProfitTypeEnum takeProfitType,
        double riskRewardRatio,
        double takeProfitTrailingCallbackPercentage,
        int entryTimes,
        string atrInterval,
        int atrLength,
        double atrMultiplier,
        bool isLong) : base(WebsocketEventTypeEnum.SAVE_QUICK_ORDER, platform)
        {
            actionToTake = WebsocketDataActionEnum.SAVE;
            this.botType = botType;
            quickOrderSetting = new WebsocketQuickOrderRequest(symbol, maxLossPercentage, maxLossAmount, weightedQuantity, quantityWeight, takeProfitType,
            riskRewardRatio, takeProfitTrailingCallbackPercentage, -1, entryTimes, atrInterval, atrLength, atrMultiplier, isLong);
        }
    }
}