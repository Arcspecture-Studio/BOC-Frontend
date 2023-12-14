using Newtonsoft.Json;
using System;

namespace Binance
{
    [Serializable]
    public class WebsocketTradeAggregateResponse : WebsocketMarketResponseData<WebsocketTradeAggregateData>
    {
    }

    [Serializable]
    public class WebsocketTradeAggregateData : WebsocketGeneralResponseData
    {
        [JsonProperty("s")]
        public string symbol;
        [JsonProperty("a")]
        public long aggregateTradeId;
        [JsonProperty("p")]
        public string price;
        [JsonProperty("q")]
        public string quantity;
        [JsonProperty("f")]
        public long firstTradeId;
        [JsonProperty("l")]
        public long lastTradeId;
        [JsonProperty("T")]
        public long tradeTime;
        [JsonProperty("m")]
        public bool isTheBuyerTheMarketMaker;
    }
}