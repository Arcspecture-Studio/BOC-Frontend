using System;

[Serializable]
public class TradingBotSetting // TODO: remove this
{
    public BotTypeEnum botType;
    public int longOrderLimit;
    public int shortOrderLimit;
    public bool autoDestroyOrder;
    public StrategySetting premiumIndex;

    public TradingBotSetting(BotTypeEnum botType, int longOrderLimit, int shortOrderLimit, bool autoDestroyOrder, StrategySetting strategySetting)
    {
        this.botType = botType;
        this.longOrderLimit = longOrderLimit;
        this.shortOrderLimit = shortOrderLimit;
        this.autoDestroyOrder = autoDestroyOrder;
        this.premiumIndex = null;
        switch (botType)
        {
            case BotTypeEnum.PREMIUM_INDEX:
                this.premiumIndex = strategySetting;
                break;
        }
    }
}
[Serializable]
public class StrategySetting { }
[Serializable]
public class PremiumIndexStrategySetting : StrategySetting
{
    public double longThresholdPercentage;
    public double shortThresholdPercentage;
    public int candleLength;

    public PremiumIndexStrategySetting(double longThresholdPercentage, double shortThresholdPercentage, int candleLength)
    {
        this.longThresholdPercentage = longThresholdPercentage;
        this.shortThresholdPercentage = shortThresholdPercentage;
        this.candleLength = candleLength;
    }
}