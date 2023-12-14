using Newtonsoft.Json;
using System;

namespace Binance
{
    [Serializable]
    public class WebsocketOrderForceResponse : WebsocketMarketResponseData<WebsocketOrderForceData>
    {
    }

    [Serializable]
    public class WebsocketOrderForceData : WebsocketGeneralResponseData
    {
        [JsonProperty("o")]
        public WebsocketOrderForceDataOrder order;
    }

    [Serializable]
    public class WebsocketOrderForceDataOrder
    {
        [JsonProperty("s")]
        public string symbol;
        [JsonProperty("S")]
        public string side;
        [JsonProperty("o")]
        public string orderType;
        [JsonProperty("f")]
        public string timeInForce;
        [JsonProperty("q")]
        public string originalQuantity;
        [JsonProperty("p")]
        public string price;
        [JsonProperty("a")]
        public string averagePrice;
        [JsonProperty("X")]
        public string orderStatus;
        [JsonProperty("l")]
        public string OrderLastFilledQuantity;
        [JsonProperty("z")]
        public string orderFilledAccumulatedQuantity;
        [JsonProperty("T")]
        public long orderTradeTime;
    }
}