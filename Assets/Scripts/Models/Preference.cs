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
    public float lossPercentage;
    public float lossAmount;
    public MarginDistributionModeEnum marginDistributionMode;
    public float marginWeightDistributionValue;
    public TakeProfitTypeEnum takeProfitType;
    public float riskRewardRatio;
    public float takeProfitQuantityPercentage;
    public float takeProfitTrailingCallbackPercentage;
    public OrderTypeEnum orderType;
    public FundingFeeHandlerEnum fundingFeeHandler;
}
[Serializable]
public class PreferenceQuickOrder
{
    public int quickEntryTimes;
    public TimeframeEnum atrTimeframe;
    public int atrLength;
    public float atrMultiplier;
}
[Serializable]
public class PreferenceBot
{
    public BotTypeEnum botType;
    public int longOrderLimit;
    public int shortOrderLimit;
    public bool autoDestroyOrder;
    public PreferenceBotPremiumIndex premiumIndex;
    public PreferenceBotMcdx mcdx;
}
[Serializable]
public class PreferenceBotPremiumIndex
{
    public float longThresholdPercentage;
    public float shortThresholdPercentage;
    public int averageCandleLength;
    public int reverseCandleBuffer;
    public int reverseCandleConfirmation;
}
[Serializable]
public class PreferenceBotMcdx
{
    public TimeframeEnum timeframe;
    public int averageCandleLength;
    public int fomoCandleConfirmation;
}