using System;

namespace General
{
    [Serializable]
    public class WebsocketSaveTradingBotRequest : WebsocketGeneralRequest
    {
        public WebsocketDataActionEnum actionToTake;
        public string botId;
        public BotTypeEnum botType;
        public QuickOrderSetting quickOrderSetting;

        public WebsocketSaveTradingBotRequest(PlatformEnum platform, string botId) : base(WebsocketEventTypeEnum.SAVE_TRADING_BOT, platform)
        {
            actionToTake = WebsocketDataActionEnum.DELETE;
            this.botId = botId;
            botType = BotTypeEnum.PREMIUM_INDEX;
            quickOrderSetting = null;
        }
        public WebsocketSaveTradingBotRequest(PlatformEnum platform, string botId,
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
        double atrMultiplier) : base(WebsocketEventTypeEnum.SAVE_TRADING_BOT, platform)
        {
            actionToTake = WebsocketDataActionEnum.SAVE;
            this.botId = botId;
            botType = BotTypeEnum.PREMIUM_INDEX;
            quickOrderSetting = new QuickOrderSetting(symbol, maxLossPercentage, maxLossAmount, weightedQuantity, quantityWeight, takeProfitType, riskRewardRatio, takeProfitTrailingCallbackPercentage, entryTimes, atrInterval, atrLength, atrMultiplier);
        }
    }
}