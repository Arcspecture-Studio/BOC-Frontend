using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CalculateMargin
{
    #region Config
    public float balance;
    public float amountToLoss;
    public int entryTimes;
    public List<float> entryPrices;
    public float stopLossPrice;
    public TakeProfitTypeEnum takeProfitType;
    public float riskRewardRatio;
    public int takeProfitQuantityPercentage;
    public float takeProfitTrailingCallbackPercentage;
    public float feeRate;
    public int quantityPrecision;
    public int pricePrecision;
    public bool weightedQuantity;
    public float quantityWeight;
    #endregion

    #region Runtime
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
    #endregion

    public CalculateMargin(float balance,
        float maxLossPercentage,
        float amountToLoss,
        int entryTimes,
        List<float> entryPrices,
        float stopLossPrice,
        TakeProfitTypeEnum takeProfitType,
        float riskRewardRatio,
        int takeProfitQuantityPercentage,
        float takeProfitTrailingCallbackPercentage,
        float feeRate,
        int quantityPrecision,
        int pricePrecision,
        bool weightedQuantity = false,
        float quantityWeight = 1)
    {
        this.balance = balance;
        this.amountToLoss = amountToLoss.Equals(float.NaN) ? balance * Utils.PercentageToRate(Mathf.Max(maxLossPercentage, 0)) : Mathf.Max(amountToLoss, 0);
        if (this.amountToLoss > this.balance) this.amountToLoss = this.balance;
        this.entryTimes = Mathf.Max(entryTimes, 0);
        this.entryPrices = entryPrices;
        for (int i = 0; i < this.entryPrices.Count; i++)
        {
            this.entryPrices[i] = Mathf.Max(this.entryPrices[i], 0);
        }
        this.stopLossPrice = Utils.RoundNDecimal(Mathf.Max(stopLossPrice, 0), pricePrecision);
        this.takeProfitType = takeProfitType;
        this.riskRewardRatio = Mathf.Max(riskRewardRatio, 0);
        this.takeProfitQuantityPercentage = takeProfitQuantityPercentage;
        this.takeProfitTrailingCallbackPercentage = takeProfitTrailingCallbackPercentage;
        this.feeRate = feeRate;
        this.quantityPrecision = quantityPrecision;
        this.pricePrecision = pricePrecision;
        this.weightedQuantity = weightedQuantity;
        this.quantityWeight = quantityWeight;
        Calculate();
    }

    void Calculate()
    {
        DeterminePositionDirection();
        CalculateEntryPrices();
        CalculateStopLossPercentage();
        CalculateEntryQuantity();
        CalculateAverageEntryPrice();
        CalculateWinPercentageAndAmount();
    }
    void DeterminePositionDirection()
    {
        isLong = entryPrices[0] > stopLossPrice;
    }
    void CalculateEntryPrices()
    {
        if (entryPrices.Count == 1 && entryTimes > 1)
        {
            float entryPriceDiff = Mathf.Abs(entryPrices[0] - stopLossPrice);
            float pricePerSection = entryPriceDiff / entryTimes;
            float initialEntryPrice = Mathf.Max(entryPrices[0], stopLossPrice);
            List<float> newEntryPrices = new();
            for (int i = 0; i <= entryTimes; i++)
            {
                float entryPrice = initialEntryPrice - pricePerSection * i;
                newEntryPrices.Add(entryPrice);
            }
            newEntryPrices.Remove(stopLossPrice);
            entryPrices = newEntryPrices;
        }
        else if (entryPrices.Count == 2 && entryTimes > 2)
        {
            float entryPriceDiff = Mathf.Abs(entryPrices[0] - entryPrices[^1]);
            float pricePerSection = entryPriceDiff / (entryTimes - 1);
            float initialEntryPrice = Mathf.Max(entryPrices[0], entryPrices[^1]);
            List<float> newEntryPrices = new();
            for (int i = 0; i < entryTimes; i++)
            {
                float entryPrice = initialEntryPrice - pricePerSection * i;
                newEntryPrices.Add(entryPrice);
            }
            entryPrices = newEntryPrices;
        }
        else
        {
            entryTimes = entryPrices.Count;
        }
        entryPrices.Sort();
        entryPrices.Reverse();
        for (int i = 0; i < entryPrices.Count; i++)
        {
            entryPrices[i] = Utils.RoundNDecimal(entryPrices[i], pricePrecision);
        }
    }
    void CalculateStopLossPercentage()
    {
        stopLossPriceGaps = new();
        entryPrices.ForEach(entryPrice =>
        {
            stopLossPriceGaps.Add(Mathf.Abs(stopLossPrice - entryPrice));
        });
        stopLossPercentage = Mathf.Abs(Utils.RateToPercentage(Utils.PriceMovingRate(entryPrices[isLong ? 0 : ^1], stopLossPrice)));
    }
    void CalculateEntryQuantity()
    {
        quantities = new();
        stopLossAmounts = new();
        fees = new();
        if (weightedQuantity)
        {
            List<float> distributions = new();
            stopLossPriceGaps.ForEach(stopLossPriceGap => distributions.Add(stopLossPriceGap / stopLossPriceGaps.Sum()));
            List<float> softmaxDistributions = Utils.ModifiedSoftmax(distributions, quantityWeight);
            for (int i = 0; i < softmaxDistributions.Count; i++)
            {
                distributions[i] = distributions[i] + (softmaxDistributions[i] - 1f / softmaxDistributions.Count);
            }
            distributions = Utils.AbsDistribution(distributions);
            for (int i = 0; i < entryPrices.Count; i++)
            {
                float feePoint = entryPrices[i] * feeRate + stopLossPrice * feeRate;
                float lossPoint = stopLossPriceGaps[i] + feePoint;
                float quantity = Utils.TruncNDecimal((amountToLoss * distributions[i]) / lossPoint, quantityPrecision);
                quantities.Add(quantity);
                stopLossAmounts.Add(quantity * lossPoint);
                fees.Add(quantity * feePoint);
            }
        }
        else
        {
            List<float> feePoints = new();
            List<float> lossPoints = new();
            for (int i = 0; i < entryPrices.Count; i++)
            {
                feePoints.Add(entryPrices[i] * feeRate + stopLossPrice * feeRate);
                lossPoints.Add(feePoints[i] + stopLossPriceGaps[i]);
            }
            float quantity = Utils.TruncNDecimal(amountToLoss / lossPoints.Sum(), quantityPrecision);
            for (int i = 0; i < entryPrices.Count; i++)
            {
                quantities.Add(quantity);
                stopLossAmounts.Add(quantity * lossPoints[i]);
                fees.Add(quantity * feePoints[i]);
            }
        }
        totalLossAmount = stopLossAmounts.Sum();
        totalFee = fees.Sum();
        balanceDecrementRate = balance == 0 ? 0 : totalLossAmount / balance;
        balanceAfterLoss = balance - totalLossAmount;
    }
    void CalculateAverageEntryPrice()
    {
        List<float> sortedEntryPrices = new(entryPrices);
        List<float> sortedQuantities = new(quantities);
        if (!isLong)
        {
            sortedEntryPrices.Reverse();
            sortedQuantities.Reverse();
        }
        float avgEntryPrice = sortedEntryPrices[0];
        avgEntryPrices = new() { Utils.RoundNDecimal(avgEntryPrice, pricePrecision) };
        float avgQuantity = sortedQuantities[0];
        cumQuantities = new() { avgQuantity };
        for (int i = 1; i < sortedEntryPrices.Count; i++)
        {
            avgEntryPrice = Utils.AveragePrice(avgEntryPrice, avgQuantity, sortedEntryPrices[i], sortedQuantities[i]);
            avgEntryPrices.Add(Utils.RoundNDecimal(avgEntryPrice, pricePrecision));
            avgQuantity += sortedQuantities[i];
            cumQuantities.Add(avgQuantity);
        }
        if (!isLong)
        {
            avgEntryPrices.Reverse();
            cumQuantities.Reverse();
        }
    }
    void CalculateWinPercentageAndAmount()
    {
        takeProfitPrices = new();
        takeProfitPricePercentages = new();
        takeProfitTrailingPrices = new();
        takeProfitTrailingPricePercentages = new();

        for (int i = 0; i < avgEntryPrices.Count; i++)
        {
            #region Calculate take profit price
            float takeProfitCumQuantity = Utils.RoundNDecimal(cumQuantities[i] * Utils.PercentageToRate(takeProfitQuantityPercentage), quantityPrecision);
            float takeProfitPrice = (isLong ? 1 : -1) *
                (avgEntryPrices[i] * takeProfitCumQuantity * feeRate + totalLossAmount * riskRewardRatio +
                (isLong ? takeProfitCumQuantity * avgEntryPrices[i] : -takeProfitCumQuantity * avgEntryPrices[i])) /
                (takeProfitCumQuantity * (isLong ? 1 - feeRate : 1 + feeRate));
            if (float.IsNaN(takeProfitPrice) || float.IsInfinity(takeProfitPrice)) takeProfitPrice = -1;
            else takeProfitPrice = Utils.RoundNDecimal(Mathf.Max(takeProfitPrice, 0), pricePrecision);
            takeProfitPrices.Add(takeProfitPrice);
            takeProfitPricePercentages.Add(Mathf.Abs(Utils.RateToPercentage(Utils.PriceMovingRate(avgEntryPrices[i], takeProfitPrice))));

            float percentage = takeProfitTrailingCallbackPercentage;
            if (isLong) percentage *= -1;
            if (takeProfitPrice < 0) takeProfitTrailingPrices.Add(takeProfitPrice);
            else
            {
                takeProfitPrice = Utils.CalculateInitialPriceByMovingPercentage(percentage, takeProfitPrice);
                takeProfitTrailingPrices.Add(Utils.RoundNDecimal(Mathf.Max(takeProfitPrice, 0), pricePrecision));
            }
            takeProfitTrailingPricePercentages.Add(Mathf.Abs(Utils.RateToPercentage(Utils.PriceMovingRate(avgEntryPrices[i], takeProfitPrice))));
            #endregion
        }

        takeProfitPrice = takeProfitPrices[isLong ? 0 : ^1];
        float cumQuantity = Utils.RoundNDecimal(cumQuantities[isLong ? 0 : ^1] * Utils.PercentageToRate(takeProfitQuantityPercentage), quantityPrecision);
        totalWinAmount = cumQuantity * Mathf.Abs(takeProfitPrice - avgEntryPrices[isLong ? 0 : ^1]) - takeProfitPrice * cumQuantity * feeRate - avgEntryPrices[isLong ? 0 : ^1] * cumQuantity * feeRate;
        winLossDiff = totalWinAmount - totalLossAmount;
        balanceIncrementRate = balance == 0 ? 0 : totalWinAmount / balance;
        balanceAfterFullWin = balance + totalWinAmount;
    }
    public void RecalculateTakeProfitPrices(TakeProfitTypeEnum takeProfitType, float riskRewardRatio,
        int takeProfitQuantityPercentage, float takeProfitTrailingCallbackPercentage) // only at frontend
    {
        this.takeProfitType = takeProfitType;
        this.riskRewardRatio = riskRewardRatio;
        this.takeProfitQuantityPercentage = takeProfitQuantityPercentage;
        this.takeProfitTrailingCallbackPercentage = takeProfitTrailingCallbackPercentage;
        CalculateWinPercentageAndAmount();
    }
}