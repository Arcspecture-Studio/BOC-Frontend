using System;

[Serializable]
public class Profile
{
    public string _id;
    public string name;
    public PlatformEnum? activePlatform;
    public ProfilePerference preference;
}
[Serializable]
public class ProfilePerference
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
    public int quickEntryTimes;
    public TimeframeEnum atrTimeframe;
    public int atrLength;
    public double atrMultiplier;
}