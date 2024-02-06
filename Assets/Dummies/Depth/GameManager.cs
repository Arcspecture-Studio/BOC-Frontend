using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using Unity.Plastic.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public RectTransform canvasRectTransform;
    public GameObject barPrefab;
    public TMP_Text middleQuantity;
    public TMP_Text largestQuantity;

    [Header("Configs")]
    public int barCount;

    [Header("Input")]
    public string jsonString;

    // Runtime
    Depth depth;
    double largestQuantityValue;

    void Start()
    {
        depth = JsonConvert.DeserializeObject<Model>(jsonString).caseStudy.depth;
        CalculateLargestQuantityValue();
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
        for (int i = 0; i < barCount; i++)
        {
            GameObject barObject = Instantiate(barPrefab);
            RectTransform rectTransform = barObject.GetComponent<RectTransform>();
            Image image = barObject.GetComponent<Image>();
            TMP_Text priceText = barObject.transform.GetChild(0).GetComponent<TMP_Text>();
            TMP_Text quantityText = barObject.transform.GetChild(1).GetComponent<TMP_Text>();

            barObject.transform.SetParent(transform);
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, canvasRectTransform.sizeDelta.y / barCount);
            if (i < 100)
            {
                image.color = Color.red;
                image.fillAmount = (float)(depth.asks[i].quantity / largestQuantityValue);
                priceText.text = depth.asks[i].price.ToString();
                quantityText.text = depth.asks[i].quantity.ToString();
            }
            else
            {
                int j = i - 100;
                image.color = Color.green;
                image.fillAmount = (float)(depth.bids[j].quantity / largestQuantityValue);
                priceText.text = depth.bids[j].price.ToString();
                quantityText.text = depth.bids[j].quantity.ToString();
            }
        }
    }
}