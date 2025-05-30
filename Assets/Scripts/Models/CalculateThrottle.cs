using UnityEngine;

public class CalculateThrottle
{
    // config
    public float currentPrice;
    public float currentQty;
    public float throttlePrice;
    public float throttleQty;
    public bool isLong;
    public float feeRate;
    public float realizedPnl;
    public float paidFundingAmount;
    public int quantityPrecision;
    public int pricePrecision;

    // runtime
    public float totalQuantity;
    public float avgPrice;
    public float breakEvenPrice;

    public CalculateThrottle(float currentPrice, float currentQty, float throttlePrice, float throttleQty, float realizedPnl, float paidFundingAmount, bool isLong, float feeRate, int quantityPrecision, int pricePrecision)
    {
        this.currentPrice = Mathf.Max(currentPrice, 0);
        this.currentQty = Mathf.Max(currentQty, 0);
        this.throttlePrice = Mathf.Max(throttlePrice, 0);
        this.throttleQty = Mathf.Max(throttleQty, 0);
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