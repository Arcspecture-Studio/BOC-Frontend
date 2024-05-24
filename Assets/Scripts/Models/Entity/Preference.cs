using System;

[Serializable]
public class Preference
{
    public PreferenceOrder order;
    public PreferenceQuickOrder quickOrder;
    public PreferenceBot bot;
}
[Serializable]
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
[Serializable]
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
}
[Serializable]
public class PreferenceBotStrategy { }
[Serializable]
public class PreferenceBotPremiumIndex : PreferenceBotStrategy
{
    public double longThresholdPercentage;
    public double shortThresholdPercentage;
    public int candleLength;
    public int reverseCandleConfirmation;
    public int fomoCandleConfirmation;
}