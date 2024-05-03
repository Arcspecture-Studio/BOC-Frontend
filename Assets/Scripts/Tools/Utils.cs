using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public static class Utils
{
    public static void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
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
    public static double PriceRatio(double initialPrice, double finalPrice)
    {
        return finalPrice / initialPrice;
    }
    public static double PriceMovingRate(double initialPrice, double finalPrice)
    {
        return PriceRatio(initialPrice, finalPrice) - 1;
    }
    public static double PercentageToRate(double a)
    {
        return a / 100;
    }
    public static double RateToPercentage(double a)
    {
        return a * 100;
    }
    public static double RoundTwoDecimal(double a)
    {
        return Math.Round(a * 100) / 100;
    }
    public static double RoundNDecimal(double a, long n)
    {
        double rounding = Math.Pow(10, n);
        return Math.Round(a * rounding) / rounding;
    }
    public static double TruncTwoDecimal(double a)
    {
        return Math.Truncate(a * 100) / 100;
    }
    public static double TruncNDecimal(double a, long n)
    {
        double rounding = Math.Pow(10, n);
        return Math.Truncate(a * rounding) / rounding;
    }
    public static long CountDecimalPlaces(double value)
    {
        string valueStr = value.ToString();
        if (valueStr.Contains("E"))
        {
            valueStr = string.Format("{0:f}", value);
        }
        if (valueStr.Contains("."))
        {
            long decimalPlaces = valueStr.Split('.')[1].Length;
            return decimalPlaces;
        }
        else
        {
            return 0;
        }
    }
    public static double AveragePrice(double price1, double ratio1, double price2, double ratio2)
    {
        if (price1 == price2) return price1;
        else if (price1 < price2)
        {
            double temp = price1;
            price1 = price2;
            price2 = temp;
            temp = ratio1;
            ratio1 = ratio2;
            ratio2 = temp;
        }
        double avgPrice = price2 + ((price1 - price2) * ratio1 / (ratio1 + ratio2));
        if (double.IsNaN(avgPrice) || double.IsInfinity(avgPrice)) avgPrice = 0;
        return avgPrice;
    }
    public static double CalculateInitialPriceByMovingPercentage(double percentage, double finalPrice)
    {
        return finalPrice / (1 + (percentage / 100));
    }
    public static long CurrentTimestamp()
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan elapsedTime = DateTime.UtcNow - epochStart;
        return (long)elapsedTime.TotalMilliseconds;
    }
    /* ModifiedSigmoid
     * Sigmoid is a generic function that takes any numbers from -infinity to infinity and squash it down to value between 0 to 1
     * Modified sigmoid here takes numbers between 0 to 1 and mapped 0 to 1 in sigmoid curve pattern
     */
    public static double ModifiedSigmoid(double value)
    {
        if (value < 0) value = 0;
        else if (value > 1) value = 1;
        return 1 / (1 + Math.Exp(-(value - 0.5) * 10));
    }
    /* ModifiedSoftmax
     * Softmax is a generic function that takes an array of numbers from -infinity to infinity and return probabilities
     * Modified softmax here takes extra parameter called alpha, the higher the value, the
     * higher the weighted probabilities it became for higher probability number
     */
    public static List<double> ModifiedSoftmax(List<double> values, double alpha)
    {
        values = values.Select(x => Math.Exp(x * alpha)).ToList();
        double sumExpValue = values.Sum();
        List<double> softmaxProbabilities = values.Select(value => value / sumExpValue).ToList();
        return softmaxProbabilities;
    }
    public static List<double> AbsDistribution(List<double> values)
    {
        while (true)
        {
            double negativeValue = 0;
            double positiveCount = 0;
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