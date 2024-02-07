using System;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DepthManager : MonoBehaviour
{
    [Header("References")]
    public RectTransform canvasRectTransform;
    public GameObject barPrefab;
    public TMP_Text middleQuantity;
    public TMP_Text largestQuantity;

    [Header("Configs")]
    public int barCount;
    public double impactMarginNotional;
    public string jsonString;

    [Header("Result")]
    public double totalQuantityOnAskSide;
    public double totalQuantityOnBidSide;
    public double totalAmountOnAskSide;
    public double totalAmountOnBidSide;
    public double impactAskPrice;
    public double impactBidPrice;
    public double impactAskPricePercentage;
    public double impactBidPricePercentage;
    public double impactAskRatio;
    public double impactBidRatio;

    // Runtime
    Depth depth;
    double largestQuantityValue;

    void Start()
    {
        depth = JsonConvert.DeserializeObject<Model>(jsonString).caseStudy.depth;
        CalculateLargestQuantityValue();
        CalculateImpactBidOrAskPrice();
        SpawnBar();
    }
    void CalculateLargestQuantityValue()
    {
        largestQuantityValue = 0;
        foreach (BarData ask in depth.asks)
        {
            if (ask.quantity > largestQuantityValue) largestQuantityValue = ask.quantity;
        }
        foreach (BarData bid in depth.bids)
        {
            if (bid.quantity > largestQuantityValue) largestQuantityValue = bid.quantity;
        }
        largestQuantity.text = largestQuantityValue.ToString();
        middleQuantity.text = (largestQuantityValue / 2).ToString();
    }
    void SpawnBar()
    {
        totalQuantityOnAskSide = 0;
        totalQuantityOnBidSide = 0;
        totalAmountOnAskSide = 0;
        totalAmountOnBidSide = 0;
        for (int i = 0; i < barCount; i++)
        {
            GameObject barObject = Instantiate(barPrefab);
            RectTransform rectTransform = barObject.GetComponent<RectTransform>();
            Image image = barObject.GetComponent<Image>();
            TMP_Text barLabel = barObject.transform.GetChild(0).GetComponent<TMP_Text>();

            barObject.transform.SetParent(transform);
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, canvasRectTransform.sizeDelta.y / barCount);
            if (i < 100)
            {
                if (Utils.TruncTwoDecimal(impactAskPrice).Equals(depth.asks[i].price))
                {
                    image.color = Color.magenta;
                }
                else
                {
                    image.color = Color.red;
                }
                image.fillAmount = (float)(depth.asks[i].quantity / largestQuantityValue);
                barLabel.text = depth.asks[i].price.ToString() + " : " + depth.asks[i].quantity.ToString();
                barObject.name = barLabel.text;
                totalQuantityOnAskSide += depth.asks[i].quantity;
                totalAmountOnAskSide += depth.asks[i].price * depth.asks[i].quantity;
            }
            else
            {
                int j = i - 100;
                if (Utils.TruncTwoDecimal(impactBidPrice).Equals(depth.bids[j].price))
                {
                    image.color = Color.cyan;
                }
                else
                {
                    image.color = Color.green;
                }
                image.fillAmount = (float)(depth.bids[j].quantity / largestQuantityValue);
                barLabel.text = depth.bids[j].price.ToString() + " : " + depth.bids[j].quantity.ToString();
                barObject.name = barLabel.text;
                totalQuantityOnBidSide += depth.bids[j].quantity;
                totalAmountOnBidSide += depth.bids[j].price * depth.bids[j].quantity;
            }
        }
    }
    void CalculateImpactBidOrAskPrice()
    {
        double remainingBalance = impactMarginNotional;
        double quantityPurchased = 0;
        for (int i = 0; i < depth.bids.Count; i++)
        {
            double amount = depth.bids[i].quantity * depth.bids[i].price;
            if (remainingBalance >= amount)
            {
                remainingBalance -= amount;
                quantityPurchased += depth.bids[i].quantity;
                continue;
            }
            else
            {
                quantityPurchased += remainingBalance / depth.bids[i].price;
                break;
            }
        }
        impactBidPrice = impactMarginNotional / quantityPurchased;

        remainingBalance = impactMarginNotional;
        quantityPurchased = 0;
        for (int i = depth.asks.Count - 1; i >= 0; i--)
        {
            double amount = depth.asks[i].quantity * depth.asks[i].price;
            if (remainingBalance >= amount)
            {
                remainingBalance -= amount;
                quantityPurchased += depth.asks[i].quantity;
                continue;
            }
            else
            {
                quantityPurchased += remainingBalance / depth.asks[i].price;
                break;
            }
        }
        impactAskPrice = impactMarginNotional / quantityPurchased;

        impactAskPricePercentage = Utils.RateToPercentage(Utils.PriceMovingRate(depth.asks[^1].price, impactAskPrice));
        impactBidPricePercentage = Utils.RateToPercentage(Utils.PriceMovingRate(depth.bids[0].price, impactBidPrice));

        impactAskRatio = 1;
        impactBidRatio = 1;
        if (Math.Abs(impactBidPricePercentage) > Math.Abs(impactAskPricePercentage))
        {
            impactBidRatio = Math.Abs(impactBidPricePercentage / impactAskPricePercentage);
        }
        else
        {
            impactAskRatio = Math.Abs(impactAskPricePercentage / impactBidPricePercentage);
        }
    }
}