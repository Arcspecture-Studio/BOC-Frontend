using System;
using System.Collections.Generic;

namespace General
{
    [Serializable]
    public class WebsocketGetInitialDataResponse : WebsocketGetRuntimeDataResponse
    {
        public string defaultProfileId;
        public WebsocketGetInitialDataAccountData accountData;
        public Dictionary<string, WebsocketGetExchangeInfo> exchangeInfos;
    }
    [Serializable]
    public class WebsocketGetInitialDataAccountData
    {
        public Dictionary<string, Profile> profiles;
        public List<PlatformEnum> platformList;
    }
    [Serializable]
    public class WebsocketGetExchangeInfo
    {
        public string symbol;
        public string marginAsset;
        public int quantityPrecision;
        public int pricePrecision;
    }
}