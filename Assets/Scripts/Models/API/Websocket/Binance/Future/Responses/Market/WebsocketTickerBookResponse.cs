using Newtonsoft.Json;
using System;

namespace Binance
{
    [Serializable]
    public class WebsocketTickerBookResponse : WebsocketMarketResponseData<WebsocketTickerBookData>
    {
    }

    [Serializable]
    public class WebsocketTickerBookData : WebsocketGeneralResponseData
    {
        [JsonProperty("u")]
        public long orderBookUpdateId;
        [JsonProperty("T")]
        public long transactionTime;
        [JsonProperty("s")]
        public string symbol;
        [JsonProperty("b")]
        public string bestBidPrice;
        [JsonProperty("B")]
        public string bestBidQty;
        [JsonProperty("a")]
        public string bestAskPrice;
        [JsonProperty("A")]
        public string bestAskQty;
    }
}