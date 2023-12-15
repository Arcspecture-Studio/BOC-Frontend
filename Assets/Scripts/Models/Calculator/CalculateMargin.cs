using System;
using System.Collections.Generic;
using System.Linq;

public class CalculateMargin
{
    // config
    public double balance;
    public double amountToLoss;
    public long entryTimes;
    public List<double> entryPrices;
    public double stopLossPrice;
    public double takeProfitPrice;
    public double riskRewardRatio;
    public double takeProfitTrailingCallbackPercentage;
    public double feeRate;
    public long quantityPrecision;
    public long pricePrecision;
    public bool weightedQuantity;
    public double quantityWeight;

    // runtime
    public bool isLong;
    public List<double> stopLossPriceGaps;
    public List<double> avgEntryPrices;
    public List<double> quantities;
    public List<double> cumQuantities;
    public List<double> stopLossAmounts;
    public List<double> fees;
    public double totalLossAmount;
    public double totalFee;
    public double balanceDecrementRate;
    public double balanceAfterLoss;
    public List<double> takeProfitPrices;
    public List<double> takeProfitTrailingPrices;
    public double totalWinAmount;
    public double balanceIncrementRate;
    public double balanceAfterFullWin;

    public CalculateMargin(double balance,
        double maxLossPercentage,
        double amountToLoss,
        long entryTimes,
        List<double> entryPrices,
        double stopLossPrice,
        double takeProfitPrice,
        double riskRewardRatio,
        double takeProfitTrailingCallbackPercentage,
        double feeRate,
        long quantityPrecision,
        long pricePrecision,
        bool weightedQuantity = false,
        double quantityWeight = 1)
    {
        this.balance = balance;
        this.amountToLoss = amountToLoss.Equals(double.NaN) ? balance * Utils.PercentageToRate(Math.Max(maxLossPercentage, 0)) : Math.Max(amountToLoss, 0);
        if (this.amountToLoss > this.balance) this.amountToLoss = this.balance;
        this.entryTimes = Math.Max(entryTimes, 0);
        this.entryPrices = entryPrices;
        for (int i = 0; i < this.entryPrices.Count; i++)
        {
            this.entryPrices[i] = Math.Max(this.entryPrices[i], 0);
        }
        this.stopLossPrice = Utils.RoundNDecimal(Math.Max(stopLossPrice, 0), pricePrecision);
        this.takeProfitPrice = takeProfitPrice;
        this.riskRewardRatio = Math.Max(riskRewardRatio, 0);
        this.takeProfitTrailingCallbackPercentage = takeProfitTrailingCallbackPercentage;
        this.feeRate = feeRate;
        this.quantityPrecision = quantityPrecision;
        this.pricePrecision = pricePrecision;
        this.weightedQuantity = weightedQuantity;
        this.quantityWeight = quantityWeight;
        Calculate();
    }

    public void Calculate()
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
        if (entryTimes > 2 && (entryPrices.Count == 1 || entryPrices.Count == 2))
        {
            List<double> newEntryPrices = new();
            double targetPrice = entryPrices.Count == 1 ? stopLossPrice : entryPrices[^1];
            double entryPriceDiff = Math.Abs(entryPrices[0] - targetPrice);
            double pricePerSection = entryPriceDiff / (entryTimes - 1);
            double initialEntryPrice = Math.Max(entryPrices[0], targetPrice);
            for (int i = 0; i < entryTimes; i++)
            {
                double entryPrice = initialEntryPrice - pricePerSection * i;
                newEntryPrices.Add(entryPrice);
            }
            if (entryPrices.Count == 1) newEntryPrices.Remove(stopLossPrice);
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
            stopLossPriceGaps.Add(Math.Abs(stopLossPrice - entryPrice));
        });
    }
    void CalculateEntryQuantity()
    {
        quantities = new();
        stopLossAmounts = new();
        fees = new();
        if (weightedQuantity)
        {
            List<double> distributions = new();
            stopLossPriceGaps.ForEach(stopLossPriceGap => distributions.Add(stopLossPriceGap / stopLossPriceGaps.Sum()));
            List<double> softmaxDistributions = Utils.ModifiedSoftmax(distributions, quantityWeight);
            for (int i = 0; i < softmaxDistributions.Count; i++)
            {
                distributions[i] = distributions[i] + (softmaxDistributions[i] - 1f / softmaxDistributions.Count);
            }
            distributions = Utils.AbsDistribution(distributions);
            for (int i = 0; i < entryPrices.Count; i++)
            {
                double feePoint = entryPrices[i] * feeRate + stopLossPrice * feeRate;
                double lossPoint = stopLossPriceGaps[i] + feePoint;
                double quantity = Utils.TruncNDecimal((amountToLoss * distributions[i]) / lossPoint, quantityPrecision);
                quantities.Add(quantity);
                stopLossAmounts.Add(quantity * lossPoint);
                fees.Add(quantity * feePoint);
            }
        }
        else
        {
            List<double> feePoints = new();
            List<double> lossPoints = new();
            for (int i = 0; i < entryPrices.Count; i++)
            {
                feePoints.Add(entryPrices[i] * feeRate + stopLossPrice * feeRate);
                lossPoints.Add(feePoints[i] + stopLossPriceGaps[i]);
            }
            double quantity = Utils.TruncNDecimal(amountToLoss / lossPoints.Sum(), quantityPrecision);
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
        List<double> sortedEntryPrices = new List<double>(entryPrices);
        List<double> sortedQuantities = new List<double>(quantities);
        if (!isLong)
        {
            sortedEntryPrices.Reverse();
            sortedQuantities.Reverse();
        }
        double avgEntryPrice = sortedEntryPrices[0];
        avgEntryPrices = new() { Utils.RoundNDecimal(avgEntryPrice, pricePrecision) };
        double avgQuantity = sortedQuantities[0];
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
        takeProfitTrailingPrices = new();

        for (int i = 0; i < avgEntryPrices.Count; i++)
        {
            double price = (isLong ? 1 : -1) *
                (avgEntryPrices[i] * cumQuantities[i] * feeRate + totalLossAmount * riskRewardRatio +
                (isLong ? cumQuantities[i] * avgEntryPrices[i] : -cumQuantities[i] * avgEntryPrices[i])) /
                (cumQuantities[i] * (isLong ? 1 - feeRate : 1 + feeRate));
            takeProfitPrices.Add(Utils.RoundNDecimal(Math.Max(price, 0), pricePrecision));

            double percentage = takeProfitTrailingCallbackPercentage;
            if (isLong) percentage *= -1;
            price = Utils.CalculateInitialPriceByMovingPercentage(percentage, price);
            takeProfitTrailingPrices.Add(Utils.RoundNDecimal(Math.Max(price, 0), pricePrecision));
        }

        takeProfitPrice = takeProfitPrices[isLong ? 0 : ^1];
        totalWinAmount = cumQuantities[isLong ? 0 : ^1] * Math.Abs(takeProfitPrice - avgEntryPrices[isLong ? 0 : ^1]) - takeProfitPrice * cumQuantities[isLong ? 0 : ^1] * feeRate - avgEntryPrices[isLong ? 0 : ^1] * cumQuantities[isLong ? 0 : ^1] * feeRate;
        balanceIncrementRate = balance == 0 ? 0 : totalWinAmount / balance;
        balanceAfterFullWin = balance + totalWinAmount;
    }
    public void RecalculateTakeProfitPrices(double riskRewardRatio,
        double takeProfitTrailingCallbackPercentage)
    {
        this.riskRewardRatio = riskRewardRatio;
        this.takeProfitTrailingCallbackPercentage = takeProfitTrailingCallbackPercentage;
        CalculateWinPercentageAndAmount();
    }
}