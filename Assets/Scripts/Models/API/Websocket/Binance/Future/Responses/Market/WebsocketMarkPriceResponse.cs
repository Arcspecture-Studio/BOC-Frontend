using Newtonsoft.Json;
using System;

namespace Binance
{
    [Serializable]
    public class WebsocketMarkPriceResponse : WebsocketMarketResponseData<WebsocketMarkPriceData>
    {
    }

    [Serializable]
    public class WebsocketMarkPriceData : WebsocketGeneralResponseData
    {
        [JsonProperty("s")]
        public string symbol;
        [JsonProperty("p")]
        public string markPrice;
        [JsonProperty("i")]
        public string indexPrice;
        [JsonProperty("P")]
        public string estimatedSettlePrice; // only useful in the last hour before the settlement starts
        [JsonProperty("r")]
        public string fundingRate;
        [JsonProperty("T")]
        public long nextFundingTime;
    }
}