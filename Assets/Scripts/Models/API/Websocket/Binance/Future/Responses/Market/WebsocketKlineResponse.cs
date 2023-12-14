using Newtonsoft.Json;
using System;

namespace Binance
{
    [Serializable]
    public class WebsocketKlineResponse : WebsocketMarketResponseData<WebsocketKlineData>
    {
    }

    [Serializable]
    public class WebsocketKlineData : WebsocketGeneralResponseData
    {
        [JsonProperty("s")]
        public string symbol;
        [JsonProperty("k")]
        public WebsocketKlineDataKline kline;
    }

    [Serializable]
    public class WebsocketKlineDataKline
    {
        [JsonProperty("t")]
        public long klineStartTime;
        [JsonProperty("T")]
        public long klineCloseTime;
        [JsonProperty("s")]
        public string symbol;
        [JsonProperty("i")]
        public string interval;
        [JsonProperty("f")]
        public long firstTradeId;
        [JsonProperty("L")]
        public long lastTradeId;
        [JsonProperty("o")]
        public string openPrice;
        [JsonProperty("c")]
        public string closePrice;
        [JsonProperty("h")]
        public string highPrice;
        [JsonProperty("l")]
        public string lowPrice;
        [JsonProperty("v")]
        public string baseAssetVolume;
        [JsonProperty("n")]
        public long numberOfTrades;
        [JsonProperty("x")]
        public bool isThisKlineClosed;
        [JsonProperty("q")]
        public string quoteAssetVolume;
        [JsonProperty("V")]
        public string takerBuyBaseAssetVolume;
        [JsonProperty("Q")]
        public string takerBuyQuoteAssetVolume;
        [JsonProperty("B")]
        public string ignore;
    }
}