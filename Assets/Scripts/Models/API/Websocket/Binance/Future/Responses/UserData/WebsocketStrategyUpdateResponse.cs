using Newtonsoft.Json;
using System;

namespace Binance
{
    [Serializable]
    public class WebsocketStrategyUpdateResponse : WebsocketUserDataResponse
    {
        [JsonProperty("T")]
        public long transactionTime;
        [JsonProperty("su")]
        public WebsocketStrategyUpdateResponseStrategyUpdate strategyUpdate;
    }

    [Serializable]
    public class WebsocketStrategyUpdateResponseStrategyUpdate
    {
        [JsonProperty("si")]
        public long strategyId;
        [JsonProperty("st")]
        public long strategyType;
        [JsonProperty("ss")]
        public long strategyStatus;
        [JsonProperty("s")]
        public long symbol;
        [JsonProperty("ut")]
        public long updateTime;
        [JsonProperty("c")]
        public long opCode;
    }
}