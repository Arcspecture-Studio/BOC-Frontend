using Newtonsoft.Json;
using System;

namespace Binance
{
    [Serializable]
    public class WebsocketGeneralResponseData
    {
        [JsonProperty("e")]
        public string eventType;
        [JsonProperty("E")]
        public long eventTime;
    }
}