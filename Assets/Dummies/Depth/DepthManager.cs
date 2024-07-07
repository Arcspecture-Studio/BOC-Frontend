using System;
using System.Collections.Generic;
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
    public WebrequestComponent webrequestComponent;

    [Header("Configs")]
    public int barCount;
    public float impactMarginNotional;
    public string jsonString;
    public bool displayCurrentDepth;

    [Header("Result")]
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

    // Runtime
    Depth depth;
    float largestQuantityValue;
    Binance.WebrequestGetDepthRequest request;

    void Start()
    {
        request = null;

        if (!displayCurrentDepth)
        {
            DisplayJsonStringData();
        }
    }
    void Update()
    {
        DisplayCurrentDepth();
        DisplayCurrentDepthResponse();
    }
    void DisplayJsonStringData()
    {
        depth = JsonConvert.DeserializeObject<Depth>(jsonString);
        CalculateLargestQuantityValue();
        CalculateImpactBidOrAskPrice();
        SpawnBar();
        GenerateResultInJsonString();
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
        bool impactAskPriceFound = false;
        bool impactBidPriceFound = false;
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
                image.color = Color.red;
                image.fillAmount = depth.asks[i].quantity / largestQuantityValue;
                barLabel.text = depth.asks[i].price.ToString() + " : " + depth.asks[i].quantity.ToString();
                barObject.name = barLabel.text;
                if (impactAskPrice >= depth.asks[i].price && !impactAskPriceFound)
                {
                    impactAskPriceFound = true;
                    barLabel.fontStyle = FontStyles.Bold;
                    barLabel.text += " <------------------------------------ Impact ASK price";
                }
            }
            else
            {
                int j = i - 100;
                image.color = Color.green;
                image.fillAmount = depth.bids[j].quantity / largestQuantityValue;
                barLabel.text = depth.bids[j].price.ToString() + " : " + depth.bids[j].quantity.ToString();
                barObject.name = barLabel.text;
                if (impactBidPrice >= depth.bids[j].price && !impactBidPriceFound)
                {
                    impactBidPriceFound = true;
                    barLabel.fontStyle = FontStyles.Bold;
                    barLabel.text += " <------------------------------------ Impact BID price";
                }
            }
        }
    }
    void DeleteBar()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    void CalculateImpactBidOrAskPrice()
    {
        totalQuantityOnAskSide = 0;
        totalQuantityOnBidSide = 0;
        totalAmountOnAskSide = 0;
        totalAmountOnBidSide = 0;
        float remainingBalance = impactMarginNotional;
        float quantityPurchased = 0;
        for (int i = 0; i < depth.bids.Count; i++)
        {
            float amount = depth.bids[i].quantity * depth.bids[i].price;
            totalAmountOnBidSide += amount;
            totalQuantityOnBidSide += depth.bids[i].quantity;
            if (remainingBalance > 0)
            {
                if (remainingBalance >= amount)
                {
                    quantityPurchased += depth.bids[i].quantity;
                    remainingBalance -= amount;
                }
                else
                {
                    quantityPurchased += remainingBalance / depth.bids[i].price;
                    remainingBalance = 0;
                }
            }
        }
        impactBidPrice = impactMarginNotional / quantityPurchased; // if balance > 0, meaning the impact price exceed current data in order book

        remainingBalance = impactMarginNotional;
        quantityPurchased = 0;
        for (int i = depth.asks.Count - 1; i >= 0; i--)
        {
            float amount = depth.asks[i].quantity * depth.asks[i].price;
            totalAmountOnAskSide += amount;
            totalQuantityOnAskSide += depth.asks[i].quantity;
            if (remainingBalance > 0)
            {
                if (remainingBalance >= amount)
                {
                    quantityPurchased += depth.asks[i].quantity;
                    remainingBalance -= amount;
                }
                else
                {
                    quantityPurchased += remainingBalance / depth.asks[i].price;
                    remainingBalance = 0;
                }
            }
        }
        impactAskPrice = impactMarginNotional / quantityPurchased;

        impactAskPricePercentage = Utils.RateToPercentage(Utils.PriceMovingRate(depth.asks[^1].price, impactAskPrice));
        impactBidPricePercentage = Utils.RateToPercentage(Utils.PriceMovingRate(depth.bids[0].price, impactBidPrice));

        impactAskRatio = 1;
        impactBidRatio = 1;
        if (Mathf.Abs(impactBidPricePercentage) > Mathf.Abs(impactAskPricePercentage))
        {
            impactBidRatio = Mathf.Abs(impactBidPricePercentage / impactAskPricePercentage);
        }
        else
        {
            impactAskRatio = Mathf.Abs(impactAskPricePercentage / impactBidPricePercentage);
        }
    }
    void GenerateResultInJsonString()
    {
        DepthResult result = new DepthResult(totalQuantityOnAskSide, totalQuantityOnBidSide, totalAmountOnAskSide, totalAmountOnBidSide, impactAskPrice, impactBidPrice, impactAskPricePercentage, impactBidPricePercentage, impactAskRatio, impactBidRatio);
        Debug.Log(JsonConvert.SerializeObject(result));
    }
    void DisplayCurrentDepth()
    {
        if (!displayCurrentDepth) return;
        if (request == null)
        {
            request = new("", "ETHUSDT", 100);
            webrequestComponent.requests.Add(request);
        }
    }
    void DisplayCurrentDepthResponse()
    {
        if (request == null) return;
        if (webrequestComponent.responses.ContainsKey(request.id))
        {
            Binance.WebrequestGetDepthResponse response = JsonConvert.DeserializeObject<Binance.WebrequestGetDepthResponse>(webrequestComponent.responses[request.id]);
            webrequestComponent.responses.Remove(request.id);
            request = null;

            List<BarData> polishedAsks = new();
            List<BarData> polishedBids = new();

            response.asks.Reverse();
            foreach (List<string> ask in response.asks)
            {
                polishedAsks.Add(new BarData(ask));
            }
            foreach (List<string> bid in response.bids)
            {
                polishedBids.Add(new BarData(bid));
            }
            depth = new Depth(polishedAsks, polishedBids);
            CalculateLargestQuantityValue();
            CalculateImpactBidOrAskPrice();
            DeleteBar();
            SpawnBar();
        }
    }
}