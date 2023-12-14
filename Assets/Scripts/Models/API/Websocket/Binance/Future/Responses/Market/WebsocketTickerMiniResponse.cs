using Newtonsoft.Json;
using System;

namespace Binance
{
    [Serializable]
    public class WebsocketTickerMiniResponse : WebsocketMarketResponseData<WebsocketTickerMiniData>
    {
    }

    [Serializable]
    public class WebsocketTickerMiniData : WebsocketGeneralResponseData
    {
        [JsonProperty("s")]
        public string symbol;
        [JsonProperty("c")]
        public string closePrice;
        [JsonProperty("o")]
        public string openPrice;
        [JsonProperty("h")]
        public string highPrice;
        [JsonProperty("l")]
        public string lowPrice;
        [JsonProperty("v")]
        public string totalTradedBaseAssetVolume;
        [JsonProperty("q")]
        public string totalTradedQuoteAssetVolume;
    }
}