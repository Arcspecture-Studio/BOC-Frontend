#pragma warning disable CS8632

using System;
using System.Collections.Generic;

namespace General
{
    [Serializable]
    public class WebsocketGetInitialDataResponse : WebsocketGetRuntimeDataResponse
    {
        public string defaultProfileId;
        public WebsocketGetInitialDataAccountData accountData;
        public object? platformData;
    }
    [Serializable]
    public class WebsocketGetInitialDataAccountData
    {
        public Dictionary<string, WebsocketGetInitialDataProfile> profiles;
        public List<PlatformEnum> platformList;
    }
    [Serializable]
    public class WebsocketGetInitialDataProfile
    {
        public string _id;
        public string name;
        public PlatformEnum activePlatform;
        public WebsocketGetInitialDataProfilePerference preference;
    }
    [Serializable]
    public class WebsocketGetInitialDataProfilePerference
    {
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
    }
}