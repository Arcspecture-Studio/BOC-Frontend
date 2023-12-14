using System;

public class CalculateThrottle
{
    // config
    public double currentPrice;
    public double currentQty;
    public double throttlePrice;
    public double throttleQty;
    public bool isLong;
    public double feeRate;
    public double realizedPnl;
    public double paidFundingAmount;
    public long quantityPrecision;
    public long pricePrecision;

    // runtime
    public double totalQuantity;
    public double avgPrice;
    public double breakEvenPrice;

    public CalculateThrottle(double currentPrice, double currentQty, double throttlePrice, double throttleQty, double realizedPnl, double paidFundingAmount, bool isLong, double feeRate, long quantityPrecision, long pricePrecision)
    {
        this.currentPrice = Math.Max(currentPrice, 0);
        this.currentQty = Math.Max(currentQty, 0);
        this.throttlePrice = Math.Max(throttlePrice, 0);
        this.throttleQty = Math.Max(throttleQty, 0);
        this.realizedPnl = realizedPnl;
        this.paidFundingAmount = paidFundingAmount;
        this.isLong = isLong;
        this.feeRate = feeRate;
        this.quantityPrecision = quantityPrecision;
        this.pricePrecision = pricePrecision;

        Calculate();
    }

    void Calculate()
    {
        totalQuantity = currentQty + throttleQty;
        avgPrice = Utils.AveragePrice(currentPrice, currentQty, throttlePrice, throttleQty);
        breakEvenPrice = (isLong ? 1 : -1) *
            (currentPrice * currentQty * feeRate +
            throttlePrice * throttleQty * feeRate + realizedPnl + paidFundingAmount +
            (isLong ? totalQuantity * avgPrice : -totalQuantity * avgPrice)) /
            (totalQuantity * (isLong ? 1 - feeRate : 1 + feeRate));

        totalQuantity = Utils.RoundNDecimal(totalQuantity, quantityPrecision);
        avgPrice = Utils.RoundNDecimal(avgPrice, pricePrecision);
        breakEvenPrice = Utils.RoundNDecimal(breakEvenPrice, pricePrecision);
    }
}