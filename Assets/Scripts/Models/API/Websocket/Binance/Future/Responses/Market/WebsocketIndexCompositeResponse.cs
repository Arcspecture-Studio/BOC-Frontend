using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebsocketIndexCompositeResponse : WebsocketMarketResponseData<WebsocketIndexCompositeData>
    {
    }

    [Serializable]
    public class WebsocketIndexCompositeData : WebsocketGeneralResponseData
    {
        [JsonProperty("s")]
        public string symbol;
        [JsonProperty("p")]
        public string price;
        [JsonProperty("C")]
        public string C;
        [JsonProperty("c")]
        public List<WebsocketIndexCompositeDataComposition> composition;
    }

    [Serializable]
    public class WebsocketIndexCompositeDataComposition
    {
        [JsonProperty("b")]
        public string baseAsset;
        [JsonProperty("q")]
        public string quoteAsset;
        [JsonProperty("w")]
        public long weightInQuantity;
        [JsonProperty("W")]
        public long weightInPercentage;
        [JsonProperty("i")]
        public long indexPrice;
    }
}