using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebsocketMarginCallResponse : WebsocketUserDataResponse
    {
        [JsonProperty("cw")]
        public string crossWalletBalance;
        [JsonProperty("p")]
        public List<WebsocketMarginCallResponsePosition> positionsOfMarginCall;
    }

    [Serializable]
    public class WebsocketMarginCallResponsePosition
    {
        [JsonProperty("s")]
        public string symbol;
        [JsonProperty("ps")]
        public string positionSide;
        [JsonProperty("pa")]
        public string positionAmount;
        [JsonProperty("mt")]
        public string marginType;
        [JsonProperty("iw")]
        public string isolatedWallet;
        [JsonProperty("mp")]
        public string markPrice;
        [JsonProperty("up")]
        public string unrealizedPnl;
        [JsonProperty("mm")]
        public string maintenanceMarginRequired;
    }
}