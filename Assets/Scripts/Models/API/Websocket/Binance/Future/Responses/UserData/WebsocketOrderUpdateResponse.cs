using Newtonsoft.Json;
using System;

namespace Binance
{
    [Serializable]
    public class WebsocketOrderUpdateResponse : WebsocketUserDataResponse
    {
        [JsonProperty("T")]
        public long transactionTime;
        [JsonProperty("o")]
        public WebsocketOrderUpdateResponseOrder order;
    }

    [Serializable]
    public class WebsocketOrderUpdateResponseOrder
    {
        [JsonProperty("s")]
        public string symbol;
        [JsonProperty("c")]
        public string clientOrderId;
        [JsonProperty("S")]
        public string side;
        [JsonProperty("o")]
        public string orderType;
        [JsonProperty("f")]
        public string timeInForce;
        [JsonProperty("q")]
        public string originalQuantity;
        [JsonProperty("p")]
        public string originalPrice;
        [JsonProperty("ap")]
        public string averagePrice;
        [JsonProperty("sp")]
        public string stopPrice; // Please ignore with TRAILING_STOP_MARKET order
        [JsonProperty("x")]
        public string executionType;
        [JsonProperty("X")]
        public string orderStatus;
        [JsonProperty("i")]
        public long orderId;
        [JsonProperty("l")]
        public string orderLastFilledQuantity;
        [JsonProperty("z")]
        public string orderFilledAccumulatedQuantity;
        [JsonProperty("L")]
        public string lastFilledPrice;
        [JsonProperty("N")]
        public string commissionAsset; // will not push if no commission
        [JsonProperty("n")]
        public string commission; // will not push if no commission
        [JsonProperty("T")]
        public long orderTradeTime;
        [JsonProperty("t")]
        public string tradeId;
        [JsonProperty("b")]
        public string bidsNotional;
        [JsonProperty("a")]
        public string askNotional;
        [JsonProperty("m")]
        public bool isThisTradeTheMakerSide;
        [JsonProperty("R")]
        public bool isThisReduceOnly;
        [JsonProperty("wt")]
        public string stopPriceWorkingType;
        [JsonProperty("ot")]
        public string originalOrderType;
        [JsonProperty("ps")]
        public string positionSide;
        [JsonProperty("cp")]
        public bool closePosition; // If Close-All, pushed with conditional order
        [JsonProperty("AP")]
        public string activationPrice; // only puhed with TRAILING_STOP_MARKET order
        [JsonProperty("cr")]
        public string callbackRate; // only puhed with TRAILING_STOP_MARKET order
        [JsonProperty("pP")]
        public bool pP;
        [JsonProperty("si")]
        public long si;
        [JsonProperty("ss")]
        public long ss;
        [JsonProperty("rp")]
        public string realizedProfit;
    }
}