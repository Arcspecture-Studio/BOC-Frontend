using System;
using System.ComponentModel;

namespace General
{
    [Serializable]
    public class TradingBotSetting<T>
    {
        public BotTypeEnum botType;
        public int longOrderLimit;
        public int shortOrderLimit;
        public bool autoDestroyOrder;
        public T strategySetting;

        public TradingBotSetting(BotTypeEnum botType, int longOrderLimit, int shortOrderLimit, bool autoDestroyOrder, T strategySetting)
        {
            this.botType = botType;
            this.longOrderLimit = longOrderLimit;
            this.shortOrderLimit = shortOrderLimit;
            this.autoDestroyOrder = autoDestroyOrder;
            this.strategySetting = strategySetting;
        }
    }
    [Serializable]
    public class PremiumIndexStrategySetting
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
}