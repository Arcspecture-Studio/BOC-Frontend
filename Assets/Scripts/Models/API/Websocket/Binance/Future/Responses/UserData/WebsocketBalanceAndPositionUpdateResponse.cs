using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebsocketBalanceAndPositionUpdateResponse : WebsocketUserDataResponse
    {
        [JsonProperty("T")]
        public long transactionTime;
        [JsonProperty("a")]
        public WebsocketBalanceAndPositionUpdateResponseUpdateData updateData;
    }

    [Serializable]
    public class WebsocketBalanceAndPositionUpdateResponseUpdateData
    {
        [JsonProperty("m")]
        public string eventReasonType;
        [JsonProperty("B")]
        public List<WebsocketBalanceAndPositionUpdateResponseBalance> balances;
        [JsonProperty("P")]
        public List<WebsocketBalanceAndPositionUpdateResponsePosition> positions;
    }

    [Serializable]
    public class WebsocketBalanceAndPositionUpdateResponseBalance
    {
        [JsonProperty("a")]
        public string asset;
        [JsonProperty("wb")]
        public string walletBalance;
        [JsonProperty("cw")]
        public string crossWalletBalance;
        [JsonProperty("bc")]
        public string balanceChange; // except PnL and Comission
    }

    [Serializable]
    public class WebsocketBalanceAndPositionUpdateResponsePosition
    {
        [JsonProperty("s")]
        public string symbol;
        [JsonProperty("pa")]
        public string positionAmount;
        [JsonProperty("ep")]
        public string entryPrice;
        [JsonProperty("cr")]
        public string accumulatedRealizedPreFee;
        [JsonProperty("up")]
        public string unrealizedPnl;
        [JsonProperty("mt")]
        public string marginType;
        [JsonProperty("iw")]
        public string isolatedWallet;
        [JsonProperty("ps")]
        public string positionSide;
    }
}