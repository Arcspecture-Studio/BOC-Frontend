using System;
using System.Collections.Generic;

[Serializable]
public class Depth
{
    public List<BarData> asks;
    public List<BarData> bids;
    public Depth() { }
    public Depth(List<BarData> asks, List<BarData> bids)
    {
        this.asks = asks;
        this.bids = bids;
    }
}
[Serializable]
public class BarData
{
    public float price;
    public float quantity;
    public BarData() { }
    public BarData(List<string> array)
    {
        price = float.Parse(array[0]);
        quantity = float.Parse(array[1]);
    }
}
[Serializable]
public class DepthResult
{
    public float totalQuantityOnAskSide;
    public float totalQuantityOnBidSide;
    public float totalAmountOnAskSide;
    public float totalAmountOnBidSide;
    public float impactAskPrice;
    public float impactBidPrice;
    public float impactAskPricePercentage;
    public float impactBidPricePercentage;
    public float impactAskRatio;
    public float impactBidRatio;

    public DepthResult(float totalQuantityOnAskSide, float totalQuantityOnBidSide, float totalAmountOnAskSide, float totalAmountOnBidSide,
    float impactAskPrice, float impactBidPrice, float impactAskPricePercentage, float impactBidPricePercentage, float impactAskRatio, float impactBidRatio)
    {
        this.totalQuantityOnAskSide = totalQuantityOnAskSide;
        this.totalQuantityOnBidSide = totalQuantityOnBidSide;
        this.totalAmountOnAskSide = totalAmountOnAskSide;
        this.totalAmountOnBidSide = totalAmountOnBidSide;
        this.impactAskPrice = impactAskPrice;
        this.impactBidPrice = impactBidPrice;
        this.impactAskPricePercentage = impactAskPricePercentage;
        this.impactBidPricePercentage = impactBidPricePercentage;
        this.impactAskRatio = impactAskRatio;
        this.impactBidRatio = impactBidRatio;
    }
}