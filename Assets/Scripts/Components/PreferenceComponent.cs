using Newtonsoft.Json.Linq;
using System;
using UnityEngine;
using WebSocketSharp;

[Serializable]
public class PreferenceComponent : MonoBehaviour
{
    public PlatformEnum tradingPlatform;
    public string symbol;
    public double lossPercentage;
    public double lossAmount;
    public MarginDistributionModeEnum marginDistributionMode;
    public double marginWeightDistributionValue;
    public TakeProfitTypeEnum takeProfitType;
    public double riskRewardRatio;
    public double takeProfitTrailingCallbackPercentage;
    public OrderTypeEnum orderType;
    public int quickEntryTimes;
    public TimeframeEnum atrTimeframe;
    public int atrLength;
    public double atrMultiplier;

    public void UpdateValue(PreferenceFile preferenceFile)
    {
        tradingPlatform = preferenceFile.tradingPlatform.GetValueOrDefault(tradingPlatform);
        symbol = preferenceFile.symbol.IsNullOrEmpty() ? symbol : preferenceFile.symbol;
        lossPercentage = preferenceFile.lossPercentage.GetValueOrDefault(lossPercentage);
        lossAmount = preferenceFile.lossAmount.GetValueOrDefault(lossAmount);
        marginDistributionMode = preferenceFile.marginDistributionMode.GetValueOrDefault(marginDistributionMode);
        marginWeightDistributionValue = preferenceFile.marginWeightDistributionValue.GetValueOrDefault(marginWeightDistributionValue);
        takeProfitType = preferenceFile.takeProfitType.GetValueOrDefault(takeProfitType);
        riskRewardRatio = preferenceFile.riskRewardRatio.GetValueOrDefault(riskRewardRatio);
        takeProfitTrailingCallbackPercentage = preferenceFile.takeProfitTrailingCallbackPercentage.GetValueOrDefault(takeProfitTrailingCallbackPercentage);
        orderType = preferenceFile.orderType.GetValueOrDefault(orderType);
        quickEntryTimes = preferenceFile.quickEntryTimes.GetValueOrDefault(quickEntryTimes);
        atrTimeframe = preferenceFile.atrTimeframe.GetValueOrDefault(atrTimeframe);
        atrLength = preferenceFile.atrLength.GetValueOrDefault(atrLength);
        atrMultiplier = preferenceFile.atrMultiplier.GetValueOrDefault(atrMultiplier);
    }
    public string GetJsonString()
    {
        JObject json = new()
        {
            { "tradingPlatform", (int)tradingPlatform },
            { "symbol", symbol },
            { "lossPercentage", lossPercentage },
            { "lossAmount", lossAmount },
            { "marginDistributionMode", (int)marginDistributionMode },
            { "marginWeightDistributionValue", marginWeightDistributionValue },
            { "takeProfitType", (int)takeProfitType },
            { "riskRewardRatio", riskRewardRatio },
            { "takeProfitTrailingCallbackPercentage", takeProfitTrailingCallbackPercentage },
            { "quickEntryTimes", quickEntryTimes },
            { "atrTimeframe", (int)atrTimeframe },
            { "atrLength", atrLength },
            { "atrMultiplier", atrMultiplier },
        };
        return json.ToString();
    }
}