using Newtonsoft.Json.Linq;
using System;
using UnityEngine;
using WebSocketSharp;

[Serializable]
public class PreferenceComponent : MonoBehaviour
{
    public double riskRewardRatio;
    public string symbol;
    public PlatformEnum tradingPlatform;
    public OrderTakeProfitTypeEnum takeProfitType;
    public float takeProfitTrailingCallbackPercentage;
    public OrderTypeEnum orderType;
    public MarginDistributionModeEnum marginDistributionMode;
    public float marginWeightDistributionValue;

    public void UpdateValue(PreferenceFile preferenceFile)
    {
        riskRewardRatio = preferenceFile.riskRewardRatio.GetValueOrDefault(riskRewardRatio);
        symbol = preferenceFile.symbol.IsNullOrEmpty() ? symbol: preferenceFile.symbol;
        tradingPlatform = preferenceFile.tradingPlatform.GetValueOrDefault(tradingPlatform);
        takeProfitType = preferenceFile.takeProfitType.GetValueOrDefault(takeProfitType);
        takeProfitTrailingCallbackPercentage = preferenceFile.takeProfitTrailingCallbackPercentage.GetValueOrDefault(takeProfitTrailingCallbackPercentage);
        orderType = preferenceFile.orderType.GetValueOrDefault(orderType);
        marginDistributionMode = preferenceFile.marginDistributionMode.GetValueOrDefault(marginDistributionMode);
        marginWeightDistributionValue = preferenceFile.marginWeightDistributionValue.GetValueOrDefault(marginWeightDistributionValue);
    }
    public string GetJsonString()
    {
        JObject json = new JObject
        {
            { "riskRewardRatio", riskRewardRatio },
            { "symbol", symbol },
            { "tradingPlatform", (int)tradingPlatform },
            { "takeProfitType", (int)takeProfitType },
            { "takeProfitTrailingCallbackPercentage", takeProfitTrailingCallbackPercentage },
            { "orderType", (int)orderType },
            { "marginDistributionMode", (int)marginDistributionMode },
            { "marginWeightDistributionValue", marginWeightDistributionValue }
        };
        return json.ToString();
    }
}