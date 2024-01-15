using System;

[Serializable]
public class PreferenceFile
{
    public PlatformEnum? tradingPlatform;
    public string symbol;
    public double? lossPercentage;
    public double? lossAmount;
    public MarginDistributionModeEnum? marginDistributionMode;
    public double? marginWeightDistributionValue;
    public OrderTakeProfitTypeEnum? takeProfitType;
    public double? riskRewardRatio;
    public double? takeProfitTrailingCallbackPercentage;
    public OrderTypeEnum? orderType;
    public int? quickEntryTimes;
    public TimeframeEnum? atrTimeframe;
    public int? atrLength;
    public double? atrMultiplier;
}