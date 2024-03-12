#pragma warning disable CS8632

using System;

namespace Binance
{
    [Serializable]
    public class WebrequestPlaceOrderResponse : WebrequestGeneralResponse
    {
        public long? orderId;
        public string? symbol;
        public string? status;
        public string? clientOrderId;
        public string? price;
        public string? avgPrice;
        public string? origQty;
        public string? executedQty;
        public string? cumQty;
        public string? activatePrice;
        public string? priceRate;
        public string? cumQuote;
        public string? timeInForce;
        public string? type;
        public string? reduceOnly;
        public string? closePosition;
        public string? side;
        public string? positionSide;
        public string? stopPrice;
        public string? workingType;
        public string? priceProtect;
        public string? origType;
        public long? updateTime;
    }
}