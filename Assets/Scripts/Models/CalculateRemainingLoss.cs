using UnityEngine;

public class CalculateRemainingLoss // Unused for now
{
    // config
    public float amountToLoss;
    public float qty;
    public float entryPrice;
    public float stopLossPrice;
    public float feeRate;

    // runtime
    public float remainingLoss;

    public CalculateRemainingLoss(float amountToLoss, float qty, float entryPrice, float stopLossPrice, float feeRate)
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
        float fee = qty * feeRate * (entryPrice + stopLossPrice);
        float lossAmount = qty * Mathf.Abs(stopLossPrice - entryPrice);
        remainingLoss = amountToLoss - lossAmount - fee;
    }
}