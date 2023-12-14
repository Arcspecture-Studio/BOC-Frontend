using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebsocketDepthPartialResponse : WebsocketMarketResponseData<WebsocketDepthPartialData>
    {
    }

    [Serializable]
    public class WebsocketDepthPartialData : WebsocketGeneralResponseData
    {
        [JsonProperty("T")]
        public long transactionTime;
        [JsonProperty("s")]
        public string symbol;
        [JsonProperty("U")]
        public long firstUpdateIdInEvent;
        [JsonProperty("u")]
        public long finalUpdateIdInEvent;
        [JsonProperty("p")]
        public long finalUpdateIdInLastStream; //ie `u` in last stream
        [JsonProperty("b")]
        public List<List<string>> bids;
        //[
        //  [
        //    "0.0024",       // Price level to be updated
        //    "10"            // Quantity
        //  ]
        //]
        [JsonProperty("a")]
        public List<List<string>> asks;
        //[
        //  [
        //    "0.0026",       // Price level to be updated
        //    "100"          // Quantity
        //  ]
        //]
    }
}