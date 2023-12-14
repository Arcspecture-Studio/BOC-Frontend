using Newtonsoft.Json;
using System;

namespace Binance
{
    [Serializable]
    public class WebsocketGridUpdateResponse : WebsocketUserDataResponse
    {
        [JsonProperty("T")]
        public long transactionTime;
        [JsonProperty("gu")]
        public WebsocketGridUpdateResponseGridUpdate gridUpdate;
    }

    [Serializable]
    public class WebsocketGridUpdateResponseGridUpdate
    {
        [JsonProperty("si")]
        public long strategyId;
        [JsonProperty("st")]
        public string strategyType;
        [JsonProperty("ss")]
        public string strategyStatus;
        [JsonProperty("s")]
        public string symbol;
        [JsonProperty("r")]
        public string realizedPnl;
        [JsonProperty("up")]
        public string unmatchedAveragePrice;
        [JsonProperty("uq")]
        public string unmatchedQty;
        [JsonProperty("uf")]
        public string unmatchedFee;
        [JsonProperty("mp")]
        public string matchedPnl;
        [JsonProperty("ut")]
        public long updateTime;
    }
}