using System;

[Serializable]
public class Preference
{
    public PreferenceOrder order;
    public PreferenceQuickOrder quickOrder;
    public PreferenceBot bot;
}
public class PreferenceOrder
{
    public string symbol;
    public double lossPercentage;
    public double lossAmount;
    public MarginDistributionModeEnum marginDistributionMode;
    public double marginWeightDistributionValue;
    public TakeProfitTypeEnum takeProfitType;
    public double riskRewardRatio;
    public double takeProfitTrailingCallbackPercentage;
    public OrderTypeEnum orderType;
}
public class PreferenceQuickOrder
{
    public int quickEntryTimes;
    public TimeframeEnum atrTimeframe;
    public int atrLength;
    public double atrMultiplier;
}
[Serializable]
public class PreferenceBot
{
    public BotTypeEnum botType;
    public int longOrderLimit;
    public int shortOrderLimit;
    public bool autoDestroyOrder;
    public PreferenceBotPremiumIndex premiumIndex;

    public PreferenceBot(BotTypeEnum botType, int longOrderLimit, int shortOrderLimit, bool autoDestroyOrder, PreferenceBotStrategy strategySetting)
    {
        this.botType = botType;
        this.longOrderLimit = longOrderLimit;
        this.shortOrderLimit = shortOrderLimit;
        this.autoDestroyOrder = autoDestroyOrder;
        this.premiumIndex = null;
        switch (botType)
        {
            case BotTypeEnum.PREMIUM_INDEX:
                this.premiumIndex = (PreferenceBotPremiumIndex)strategySetting;
                break;
        }
    }
}
[Serializable]
public class PreferenceBotStrategy { }
[Serializable]
public class PreferenceBotPremiumIndex : PreferenceBotStrategy
{
    public double longThresholdPercentage;
    public double shortThresholdPercentage;
    public int candleLength;

    public PreferenceBotPremiumIndex(double longThresholdPercentage, double shortThresholdPercentage, int candleLength)
    {
        this.longThresholdPercentage = longThresholdPercentage;
        this.shortThresholdPercentage = shortThresholdPercentage;
        this.candleLength = candleLength;
    }
}