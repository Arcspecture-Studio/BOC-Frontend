using System;

namespace General
{
    [Serializable]
    public class WebsocketSaveTradingBotRequest : WebsocketGeneralRequest
    {
        public WebsocketDataActionEnum actionToTake;
        public string botId;
        public BotTypeEnum botType;
        public WebsocketQuickOrderSettingRequest quickOrderSetting;

        public WebsocketSaveTradingBotRequest(PlatformEnum platform) : base(WebsocketEventTypeEnum.SAVE_TRADING_BOT, platform)
        {
            actionToTake = WebsocketDataActionEnum.DELETE;
            botId = "1"; // PENDING: To be improve
            botType = BotTypeEnum.PREMIUM_INDEX;
            quickOrderSetting = null;
        }
        public WebsocketSaveTradingBotRequest(PlatformEnum platform,
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
            botId = "1";
            botType = BotTypeEnum.PREMIUM_INDEX;
            quickOrderSetting = new WebsocketQuickOrderSettingRequest(symbol, maxLossPercentage, maxLossAmount, weightedQuantity, quantityWeight, takeProfitType,
            riskRewardRatio, takeProfitTrailingCallbackPercentage, entryTimes, atrInterval, atrLength, atrMultiplier, true);
        }
    }
    [Serializable]
    public class WebsocketQuickOrderSettingRequest
    {
        public string symbol;
        public double maxLossPercentage; // 0 means null
        public double maxLossAmount; // 0 means null
        public bool weightedQuantity;
        public double quantityWeight;
        public OrderTakeProfitTypeEnum takeProfitType;
        public double riskRewardRatio;
        public double takeProfitTrailingCallbackPercentage;
        public int entryTimes;
        public string atrInterval;
        public int atrLength;
        public double atrMultiplier;
        public bool autoDestroy;

        public WebsocketQuickOrderSettingRequest(string symbol,
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
        bool autoDestroy)
        {
            this.symbol = symbol;
            this.maxLossPercentage = maxLossPercentage;
            this.maxLossAmount = maxLossAmount;
            this.weightedQuantity = weightedQuantity;
            this.quantityWeight = quantityWeight;
            this.takeProfitType = takeProfitType;
            this.riskRewardRatio = riskRewardRatio;
            this.takeProfitTrailingCallbackPercentage = takeProfitTrailingCallbackPercentage;
            this.entryTimes = entryTimes;
            this.atrInterval = atrInterval;
            this.atrLength = atrLength;
            this.atrMultiplier = atrMultiplier;
            this.autoDestroy = autoDestroy;
        }
    }
}