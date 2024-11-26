using System.Collections.Generic;

public class MarginCalculatorUpdate
{
    public TakeProfitTypeEnum takeProfitType;
    public float riskRewardRatio;
    public int takeProfitQuantityPercentage;
    public float takeProfitTrailingCallbackPercentage;

    public MarginCalculatorUpdate(TakeProfitTypeEnum takeProfitType, float riskRewardRatio,
    int takeProfitQuantityPercentage, float takeProfitTrailingCallbackPercentage)
    {
        this.takeProfitType = takeProfitType;
        this.riskRewardRatio = riskRewardRatio;
        this.takeProfitQuantityPercentage = takeProfitQuantityPercentage;
        this.takeProfitTrailingCallbackPercentage = takeProfitTrailingCallbackPercentage;
    }
}
public class MarginCalculatorConfig : MarginCalculatorUpdate
{
    public float balance;
    public float amountToLoss;
    public int entryTimes;
    public List<float> entryPrices;
    public float stopLossPrice;
    public float feeRate;
    public int quantityPrecision;
    public int pricePrecision;
    public bool weightedQuantity;
    public float quantityWeight;

    public MarginCalculatorConfig(float balance, float amountToLoss, int entryTimes,
    List<float> entryPrices, float stopLossPrice, float feeRate, int quantityPrecision,
    int pricePrecision, bool weightedQuantity, float quantityWeight,
    TakeProfitTypeEnum takeProfitType, float riskRewardRatio,
    int takeProfitQuantityPercentage, float takeProfitTrailingCallbackPercentage) : base(
        takeProfitType, riskRewardRatio, takeProfitQuantityPercentage,
        takeProfitTrailingCallbackPercentage)
    {
        this.balance = balance;
        this.amountToLoss = amountToLoss;
        this.entryTimes = entryTimes;
        this.entryPrices = entryPrices;
        this.stopLossPrice = stopLossPrice;
        this.feeRate = feeRate;
        this.quantityPrecision = quantityPrecision;
        this.pricePrecision = pricePrecision;
        this.weightedQuantity = weightedQuantity;
        this.quantityWeight = quantityWeight;
    }
}
public class MarginCalculatorAdd : MarginCalculatorConfig
{
    public float maxLossPercentage;

    public MarginCalculatorAdd(float balance, float maxLossPercentage, float amountToLoss, int entryTimes,
    List<float> entryPrices, float stopLossPrice, float feeRate, int quantityPrecision,
    int pricePrecision, bool weightedQuantity, float quantityWeight,
    TakeProfitTypeEnum takeProfitType, float riskRewardRatio,
    int takeProfitQuantityPercentage, float takeProfitTrailingCallbackPercentage) : base(
        balance, amountToLoss, entryTimes, entryPrices, stopLossPrice, feeRate, quantityPrecision,
        pricePrecision, weightedQuantity, quantityWeight, takeProfitType, riskRewardRatio,
        takeProfitQuantityPercentage, takeProfitTrailingCallbackPercentage)
    {
        this.maxLossPercentage = maxLossPercentage;
    }

    public MarginCalculatorUpdate GetMarginCalculatorUpdate()
    {
        return new MarginCalculatorUpdate(takeProfitType, riskRewardRatio, takeProfitQuantityPercentage, takeProfitTrailingCallbackPercentage);
    }
    public void UpdateMarginCalculatorFromUI(int takeProfitType, string riskRewardRatio, float takeProfitQuantityPercentage, float takeProfitTrailingCallbackPercentage)
    {
        this.takeProfitType = (TakeProfitTypeEnum)takeProfitType;
        this.riskRewardRatio = float.Parse(riskRewardRatio);
        this.takeProfitQuantityPercentage = (int)takeProfitQuantityPercentage;
        this.takeProfitTrailingCallbackPercentage = takeProfitTrailingCallbackPercentage;
    }
}
public class MarginCalculator : MarginCalculatorConfig
{
    public bool isLong;
    public List<float> stopLossPriceGaps;
    public float stopLossPercentage;
    public List<float> avgEntryPrices;
    public List<float> quantities;
    public List<float> cumQuantities;
    public List<float> stopLossAmounts;
    public List<float> fees;
    public float totalLossAmount;
    public float totalFee;
    public float balanceDecrementRate;
    public float balanceAfterLoss;
    public float takeProfitPrice;
    public List<float> takeProfitPrices;
    public List<float> takeProfitPricePercentages;
    public List<float> takeProfitTrailingPrices;
    public List<float> takeProfitTrailingPricePercentages;
    public float totalWinAmount;
    public float balanceIncrementRate;
    public float balanceAfterFullWin;
    public float winLossDiff;

    public MarginCalculator(float balance, float amountToLoss, int entryTimes,
    List<float> entryPrices, float stopLossPrice, float feeRate, int quantityPrecision,
    int pricePrecision, bool weightedQuantity, float quantityWeight,
    TakeProfitTypeEnum takeProfitType, float riskRewardRatio,
    int takeProfitQuantityPercentage, float takeProfitTrailingCallbackPercentage) : base(
        balance, amountToLoss, entryTimes, entryPrices, stopLossPrice, feeRate, quantityPrecision,
        pricePrecision, weightedQuantity, quantityWeight, takeProfitType, riskRewardRatio,
        takeProfitQuantityPercentage, takeProfitTrailingCallbackPercentage)
    {
    }
}