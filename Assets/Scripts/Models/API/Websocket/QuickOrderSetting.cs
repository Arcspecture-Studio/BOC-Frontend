using System;

[Serializable]
public class QuickOrderSetting
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

    public QuickOrderSetting(string symbol,
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
    double atrMultiplier)
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
    }
}