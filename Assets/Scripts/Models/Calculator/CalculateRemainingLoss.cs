using System;

public class CalculateRemainingLoss
{
    // config
    public double amountToLoss;
    public double qty;
    public double entryPrice;
    public double stopLossPrice;
    public double feeRate;

    // runtime
    public double remainingLoss;

    public CalculateRemainingLoss(double amountToLoss, double qty, double entryPrice, double stopLossPrice, double feeRate)
    {
        this.amountToLoss = amountToLoss;
        this.qty = qty;
        this.entryPrice = entryPrice;
        this.stopLossPrice = stopLossPrice;
        this.feeRate = feeRate;

        Calculate();
    }

    void Calculate()
    {
        double fee = qty * feeRate * (entryPrice + stopLossPrice);
        double lossAmount = qty * Math.Abs(stopLossPrice - entryPrice);
        remainingLoss = amountToLoss - lossAmount - fee;
    }
}