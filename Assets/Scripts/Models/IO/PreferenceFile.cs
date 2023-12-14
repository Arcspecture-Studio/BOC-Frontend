using System;

[Serializable]
public class PreferenceFile
{
    public double? riskRewardRatio;
    public string symbol;
    public PlatformEnum? tradingPlatform;
    public OrderTakeProfitTypeEnum? takeProfitType;
    public float? takeProfitTrailingCallbackPercentage;
    public OrderTypeEnum? orderType;
    public MarginDistributionModeEnum? marginDistributionMode;
    public float? marginWeightDistributionValue;
}