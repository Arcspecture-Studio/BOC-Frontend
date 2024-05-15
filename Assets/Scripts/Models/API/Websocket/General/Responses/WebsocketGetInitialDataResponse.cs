using System;
using System.Collections.Generic;

namespace General
{
    [Serializable]
    public class WebsocketGetInitialDataResponse : WebsocketGetProfileDataResponse
    {
        public Dictionary<string, WebsocketGetExchangeInfo> exchangeInfos;
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