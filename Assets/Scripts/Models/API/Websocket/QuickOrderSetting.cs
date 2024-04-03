using System;

[Serializable]
public class QuickOrderSetting
{
    public string symbol;
    public double maxLossPercentage; // 0 means null
    public double maxLossAmount; // 0 means null
    public bool weightedQuantity;
    public double quantityWeight;
    public TakeProfitTypeEnum takeProfitType;
    public OrderTypeEnum orderType;
    public double riskRewardRatio;
    public double takeProfitTrailingCallbackPercentage;
    public int entryTimes;
    public TimeframeEnum atrTimeframe;
    public int atrLength;
    public double atrMultiplier;

    public QuickOrderSetting(string symbol,
    double maxLossPercentage,
    double maxLossAmount,
    bool weightedQuantity,
    double quantityWeight,
    TakeProfitTypeEnum takeProfitType,
    OrderTypeEnum orderType,
    double riskRewardRatio,
    double takeProfitTrailingCallbackPercentage,
    int entryTimes,
    TimeframeEnum atrTimeframe,
    int atrLength,
    double atrMultiplier)
    {
        this.symbol = symbol;
        this.maxLossPercentage = maxLossPercentage;
        this.maxLossAmount = maxLossAmount;
        this.weightedQuantity = weightedQuantity;
        this.quantityWeight = quantityWeight;
        this.takeProfitType = takeProfitType;
        this.orderType = orderType;
        this.riskRewardRatio = riskRewardRatio;
        this.takeProfitTrailingCallbackPercentage = takeProfitTrailingCallbackPercentage;
        this.entryTimes = entryTimes;
        this.atrTimeframe = atrTimeframe;
        this.atrLength = atrLength;
        this.atrMultiplier = atrMultiplier;
    }
}