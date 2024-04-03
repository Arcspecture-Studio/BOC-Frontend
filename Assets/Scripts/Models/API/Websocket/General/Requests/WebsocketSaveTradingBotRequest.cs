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

        public WebsocketSaveTradingBotRequest(string botId) : base(WebsocketEventTypeEnum.SAVE_TRADING_BOT)
        {
            actionToTake = WebsocketDataActionEnum.DELETE;
            this.botId = botId;
            botType = BotTypeEnum.PREMIUM_INDEX;
            quickOrderSetting = null;
        }
        public WebsocketSaveTradingBotRequest(string botId,
        string symbol,
        double maxLossPercentage,
        double maxLossAmount,
        bool weightedQuantity,
        double quantityWeight,
        TakeProfitTypeEnum takeProfitType,
        OrderTypeEnum orderType,
        double riskRewardRatio,
        double takeProfitTrailingCallbackPercentage,
        int entryTimes,
        TimeframeEnum atrTimeframe,
        int atrLength,
        double atrMultiplier) : base(WebsocketEventTypeEnum.SAVE_TRADING_BOT)
        {
            actionToTake = WebsocketDataActionEnum.SAVE;
            this.botId = botId;
            botType = BotTypeEnum.PREMIUM_INDEX;
            quickOrderSetting = new QuickOrderSetting(symbol, maxLossPercentage, maxLossAmount, weightedQuantity, quantityWeight, takeProfitType,
            orderType, riskRewardRatio, takeProfitTrailingCallbackPercentage, entryTimes, atrTimeframe, atrLength, atrMultiplier);
        }
    }
}