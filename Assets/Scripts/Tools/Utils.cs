using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public static class Utils
{
    public static bool IsJsonString(string str)
    {
        try
        {
            JsonConvert.DeserializeObject(str);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
    public static float PriceRatio(float initialPrice, float finalPrice)
    {
        return finalPrice / initialPrice;
    }
    public static float PriceMovingRate(float initialPrice, float finalPrice)
    {
        return PriceRatio(initialPrice, finalPrice) - 1;
    }
    public static float PercentageToRate(float a)
    {
        return a / 100;
    }
    public static float RateToPercentage(float a)
    {
        return a * 100;
    }
    public static float RoundTwoDecimal(float a)
    {
        return Mathf.Round(a * 100) / 100f;
    }
    public static float RoundNDecimal(float a, int n)
    {
        float rounding = Mathf.Pow(10, n);
        return Mathf.Round(a * rounding) / rounding;
    }
    public static float TruncTwoDecimal(float a)
    {
        return Mathf.FloorToInt(a * 100) / 100f;
    }
    public static float TruncNDecimal(float a, int n)
    {
        float rounding = Mathf.Pow(10, n);
        return Mathf.FloorToInt(a * rounding) / rounding;
    }
    public static int CountDecimalPlaces(float value)
    {
        string valueStr = value.ToString();
        if (valueStr.Contains("E"))
        {
            valueStr = string.Format("{0:f}", value);
        }
        if (valueStr.Contains("."))
        {
            int decimalPlaces = valueStr.Split('.')[1].Length;
            return decimalPlaces;
        }
        else
        {
            return 0;
        }
    }
    public static float GetDecimalPlaceNumber(int decimalPlace)
    {
        string valueStr = "0.";
        for (int i = 0; i < decimalPlace; i++)
        {
            if (i == decimalPlace - 1)
            {
                valueStr += "1";
            }
            else
            {
                valueStr += "0";
            }
        }
        return float.Parse(valueStr);
    }
    public static float AveragePrice(float price1, float ratio1, float price2, float ratio2)
    {
        if (price1 == price2) return price1;
        else if (price1 < price2)
        {
            float temp = price1;
            price1 = price2;
            price2 = temp;
            temp = ratio1;
            ratio1 = ratio2;
            ratio2 = temp;
        }
        float avgPrice = price2 + ((price1 - price2) * ratio1 / (ratio1 + ratio2));
        if (float.IsNaN(avgPrice) || float.IsInfinity(avgPrice)) avgPrice = 0;
        return avgPrice;
    }
    public static float CalculateInitialPriceByMovingPercentage(float percentage, float finalPrice)
    {
        return finalPrice / (1 + (percentage / 100));
    }
    public static long CurrentTimestamp() // TODO: investigate why it return negative value, probably has something to do with int becuase it should have been long
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan elapsedTime = DateTime.UtcNow - epochStart;
        return (long)elapsedTime.TotalMilliseconds;
    }
    /* ModifiedSigmoid
     * Sigmoid is a generic function that takes any numbers from -infinity to infinity and squash it down to value between 0 to 1
     * Modified sigmoid here takes numbers between 0 to 1 and mapped 0 to 1 in sigmoid curve pattern
     */
    public static float ModifiedSigmoid(float value)
    {
        if (value < 0) value = 0;
        else if (value > 1) value = 1;
        return 1 / (1 + Mathf.Exp(-(value - 0.5f) * 10));
    }
    /* ModifiedSoftmax
     * Softmax is a generic function that takes an array of numbers from -infinity to infinity and return probabilities
     * Modified softmax here takes extra parameter called alpha, the higher the value, the
     * higher the weighted probabilities it became for higher probability number
     */
    public static List<float> ModifiedSoftmax(List<float> values, float alpha)
    {
        values = values.Select(x => Mathf.Exp(x * alpha)).ToList();
        float sumExpValue = values.Sum();
        List<float> softmaxProbabilities = values.Select(value => value / sumExpValue).ToList();
        return softmaxProbabilities;
    }
    public static List<float> AbsDistribution(List<float> values)
    {
        while (true)
        {
            float negativeValue = 0;
            float positiveCount = 0;
            for (int i = 0; i < values.Count; i++)
            {
                if (values[i] < 0)
                {
                    negativeValue += values[i];
                    values[i] = 0;
                }
                else if (values[i] > 0)
                {
                    positiveCount++;
                }
            }
            if (negativeValue >= 0) break;

            negativeValue /= positiveCount;
            for (int i = 0; i < values.Count; i++)
            {
                if (values[i] > 0)
                {
                    values[i] += negativeValue;
                }
            }
        }
        return values;
    }
}