using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebsocketInfoContractResponse : WebsocketMarketResponseData<WebsocketInfoContractData>
    {
    }

    [Serializable]
    public class WebsocketInfoContractData : WebsocketGeneralResponseData
    {
        [JsonProperty("s")]
        public string symbol;
        [JsonProperty("ps")]
        public string pair;
        [JsonProperty("ct")]
        public string contractType;
        [JsonProperty("dt")]
        public long deliveryDateTime;
        [JsonProperty("ot")]
        public long onboardDateTime;
        [JsonProperty("cs")]
        public string contractStatus;
        [JsonProperty("bks")]
        public List<WebsocketInfoContractDataBks> bks;
    }

    [Serializable]
    public class WebsocketInfoContractDataBks
    {
        [JsonProperty("bs")]
        public long notionalBracket;
        [JsonProperty("bnf")]
        public long floorNotionalOfThisBracket;
        [JsonProperty("bnc")]
        public long capNotionalOfThisBracket;
        [JsonProperty("mmr")]
        public long maintenanceRatioOfThisBracket;
        [JsonProperty("cf")]
        public long auxilaryNumberForQuickCalculation;
        [JsonProperty("mi")]
        public long minLeverageForThisBracket;
        [JsonProperty("ma")]
        public long maxLeverageForThisBracket;
    }
}