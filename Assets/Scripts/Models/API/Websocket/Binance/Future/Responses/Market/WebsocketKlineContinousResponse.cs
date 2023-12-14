using Newtonsoft.Json;
using System;

namespace Binance
{
    [Serializable]
    public class WebsocketKlineContinuousResponse : WebsocketMarketResponseData<WebsocketKlineContinuousData>
    {
    }

    [Serializable]
    public class WebsocketKlineContinuousData : WebsocketGeneralResponseData
    {
        [JsonProperty("ps")]
        public string pair;
        [JsonProperty("ct")]
        public string contractType;
        [JsonProperty("k")]
        public WebsocketKlineDataKline kline;
    }
}