using System;
using System.Collections.Generic;

namespace General
{
    [Serializable]
    public class WebsocketGetInitialDataResponse<T> : WebsocketGeneralResponse
    {
        public string defaultProfileId;
        public WebsocketGetInitialDataAccountData accountData;
        public T platformData;
    }
    [Serializable]
    public class WebsocketGetInitialDataAccountData
    {
        public Dictionary<string, WebsocketGetInitialDataProfileData> profiles;
        public List<PlatformEnum> platformList;
    }
    [Serializable]
    public class WebsocketGetInitialDataProfileData
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