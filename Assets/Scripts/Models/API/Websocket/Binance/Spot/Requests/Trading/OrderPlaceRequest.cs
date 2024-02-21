#pragma warning disable CS8632

using System;
using UnityEngine;

namespace Binance
{
    [Serializable]
    public class OrderPlaceRequest : RequestWrapper<OrderPlaceParams>
    {
        public OrderPlaceRequest(string method, OrderPlaceParams param) : base(method, param)
        {
        }
    }

    [Serializable]
    public class OrderPlaceParams
    {
        public string symbol;
        public string side;
        public string type;
        public string? timeInForce;
        public string? price;
        public string? quantity;
        public string? quoteOrderQty;
        public string? newClientOrderId;
        public string? newOrderRespType;
        public string? stopPrice;
        public long? trailingDelta;
        public string? icebergQty;
        public long? strategyId;
        public long? strategyType;
        public string? selfTradePreventionMode;
        public string apiKey;
        public long? recvWindow;
        public string signature;
        public long timestamp;

        public OrderPlaceParams(string symbol, string side, string apiKey)
        {
            this.symbol = symbol;
            this.side = side;
            this.apiKey = apiKey;
        }
        public void SetTypeLimit(string timeInForce, double price, double quantity)
        {
            this.type = RequestOrderTypeEnum.LIMIT;
            this.timeInForce = timeInForce;
            this.price = price.ToString();
            this.quantity = quantity.ToString();
        }
        public void SetTypeLimitMaker(double price, double quantity)
        {
            this.type = RequestOrderTypeEnum.LIMIT_MAKER;
            this.price = price.ToString();
            this.quantity = quantity.ToString();
        }
        public void SetTypeMarket(double quantity)
        {
            this.type = RequestOrderTypeEnum.MARKET;
            this.quantity = quantity.ToString();
        }
        public void SetStopLoss(double quantity, double stopPrice)
        {
            this.type = RequestOrderTypeEnum.STOP_LOSS;
            this.quantity = quantity.ToString();
            this.stopPrice = stopPrice.ToString();
        }
        public void SetStopLossLimit(string timeInForce, double price, double quantity, double stopPrice)
        {
            this.type = RequestOrderTypeEnum.STOP_LOSS_LIMIT;
            this.timeInForce = timeInForce;
            this.price = price.ToString();
            this.quantity = quantity.ToString();
            this.stopPrice = stopPrice.ToString();
        }
        public void SetTakeProfit(double quantity, double stopPrice)
        {
            this.type = RequestOrderTypeEnum.TAKE_PROFIT;
            this.quantity = quantity.ToString();
            this.stopPrice = stopPrice.ToString();
        }
        public void SetTakeProfitLimit(string timeInForce, double price, double quantity, double stopPrice)
        {
            this.type = RequestOrderTypeEnum.TAKE_PROFIT_LIMIT;
            this.timeInForce = timeInForce;
            this.price = price.ToString();
            this.quantity = quantity.ToString();
            this.stopPrice = stopPrice.ToString();
        }
        public string GetQueryParams()
        {
            return "apiKey=" + this.apiKey
                + (icebergQty != null ? "&icebergQty=" + this.icebergQty : "")
                + (newClientOrderId != null ? "&newClientOrderId=" + this.newClientOrderId : "")
                + (newOrderRespType != null ? "&newOrderRespType=" + this.newOrderRespType : "")
                + (price != null ? "&price=" + this.price : "")
                + (quantity != null ? "&quantity=" + this.quantity : "")
                + (quoteOrderQty != null ? "&quoteOrderQty=" + this.quoteOrderQty : "")
                + (recvWindow != null ? "&recvWindow=" + this.recvWindow : "")
                + (selfTradePreventionMode != null ? "&selfTradePreventionMode=" + this.selfTradePreventionMode : "")
                + "&side=" + this.side
                + (stopPrice != null ? "&stopPrice=" + this.stopPrice : "")
                + (strategyId != null ? "&strategyId=" + this.strategyId : "")
                + (strategyType != null ? "&strategyType=" + this.strategyType : "")
                + "&symbol=" + this.symbol
                + (timeInForce != null ? "&timeInForce=" + this.timeInForce : "")
                + "&timestamp=" + this.timestamp
                + (trailingDelta != null ? "&trailingDelta=" + this.trailingDelta : "")
                + "&type=" + this.type;
        }
        public void SetSignature(string apiSecret)
        {
            this.timestamp = Utils.CurrentTimestamp();
            string queryString = GetQueryParams();
            Debug.Log("Query: " + queryString);
            this.signature = Signature.Binance(queryString, apiSecret);
        }
    }
}