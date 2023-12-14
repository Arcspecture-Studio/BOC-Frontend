using Newtonsoft.Json;
using System;

namespace Binance
{
    [Serializable]
    public class WebsocketTickerResponse : WebsocketMarketResponseData<WebsocketTickerData>
    {
    }

    [Serializable]
    public class WebsocketTickerData : WebsocketGeneralResponseData
    {
        [JsonProperty("s")]
        public string symbol;
        [JsonProperty("s")]
        public string priceChange;
        [JsonProperty("s")]
        public string priceChangePercent;
        [JsonProperty("s")]
        public string weightedAveragePrice;
        [JsonProperty("s")]
        public string lastPrice;
        [JsonProperty("s")]
        public string lastQuantity;
        [JsonProperty("s")]
        public string openPrice;
        [JsonProperty("s")]
        public string highPrice;
        [JsonProperty("s")]
        public string lowPrice;
        [JsonProperty("s")]
        public string totalTradedBaseAssetVolume;
        [JsonProperty("s")]
        public string totalTradedQuoteAssetVolume;
        [JsonProperty("s")]
        public long statisticsOpenTime;
        [JsonProperty("s")]
        public long statisticsCloseTime;
        [JsonProperty("s")]
        public long firstTradeId;
        [JsonProperty("s")]
        public long lastTradeId;
        [JsonProperty("s")]
        public long totalNumberOfTrades;
    }
}