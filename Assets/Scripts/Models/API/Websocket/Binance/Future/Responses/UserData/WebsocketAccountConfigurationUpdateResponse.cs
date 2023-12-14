using Newtonsoft.Json;
using System;

namespace Binance
{
    [Serializable]
    public class WebsocketAccountConfigurationUpdateResponse : WebsocketUserDataResponse
    {
        [JsonProperty("T")]
        public long transactionTime;
        [JsonProperty("ac")]
        public WebsocketAccountConfigurationUpdateResponseTradePair tradePair;
        [JsonProperty("ai")]
        public WebsocketAccountConfigurationUpdateResponseUserAccountConfig userAccountConfig;
    }

    [Serializable]
    public class WebsocketAccountConfigurationUpdateResponseTradePair
    {
        [JsonProperty("s")]
        public string symbol;
        [JsonProperty("l")]
        public long leverage;
    }

    [Serializable]
    public class WebsocketAccountConfigurationUpdateResponseUserAccountConfig
    {
        [JsonProperty("j")]
        public bool multiAssetsMode;
    }
}