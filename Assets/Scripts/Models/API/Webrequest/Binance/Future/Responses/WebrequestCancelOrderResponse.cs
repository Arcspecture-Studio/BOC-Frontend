#pragma warning disable CS8632

using System;

namespace Binance
{
    [Serializable]
    public class WebrequestCancelOrderResponse : WebrequestGeneralResponse
    {
        public string? clientOrderId;
        public string? cumQty;
        public string? cumQuote;
        public string? executedQty;
        public long? orderId;
        public string? origQty;
        public string? origType;
        public string? price;
        public bool? reduceOnly;
        public string? side;
        public string? positionSide;
        public string? status;
        public string? stopPrice;
        public bool? closePosition;
        public string? symbol;
        public string? timeInForce;
        public string? type;
        public string? activatePrice;
        public string? priceRate;
        public long? updateTime;
        public string? workingType;
        public bool? priceProtect;
    }
}