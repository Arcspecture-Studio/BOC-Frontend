using System;

namespace Binance
{
    [Serializable]
    public class WebsocketOrder : OrderContent
    {
        public string symbol;
        public string side;
        public string positionSide;
        public string type;
        public string quantity;

        public WebsocketOrder(string symbol, string side, string positionSide, string type, double? quantity)
        {
            this.symbol = symbol;
            this.side = side;
            this.positionSide = positionSide;
            this.type = type;
            this.quantity = quantity?.ToString();
        }
    }

    #region Market order
    [Serializable]
    public class WebsocketMarketOrder : WebsocketOrder
    {
        public WebsocketMarketOrder(string symbol, string side, string positionSide, string type, double quantity) : base(symbol, side, positionSide, type, quantity)
        {
        }
    }

    [Serializable]
    public class WebsocketMarketOpenLongOrder : WebsocketMarketOrder
    {
        public WebsocketMarketOpenLongOrder(string symbol, double quantity) : base(symbol, "BUY", "LONG", "MARKET", quantity)
        {
        }
    }

    [Serializable]
    public class WebsocketMarketCloseLongOrder : WebsocketMarketOrder
    {
        public WebsocketMarketCloseLongOrder(string symbol, double quantity) : base(symbol, "SELL", "LONG", "MARKET", quantity)
        {
        }
    }

    [Serializable]
    public class WebsocketMarketOpenShortOrder : WebsocketMarketOrder
    {
        public WebsocketMarketOpenShortOrder(string symbol, double quantity) : base(symbol, "SELL", "SHORT", "MARKET", quantity)
        {
        }
    }

    [Serializable]
    public class WebsocketMarketCloseShortOrder : WebsocketMarketOrder
    {
        public WebsocketMarketCloseShortOrder(string symbol, double quantity) : base(symbol, "BUY", "SHORT", "MARKET", quantity)
        {
        }
    }
    #endregion

    #region Limit order
    [Serializable]
    public class WebsocketLimitOrder : WebsocketOrder
    {
        public string timeInForce;
        public string price;

        public WebsocketLimitOrder(string symbol, string side, string positionSide, string type, double quantity, string timeInForce, double price) : base(symbol, side, positionSide, type, quantity)
        {
            this.timeInForce = timeInForce;
            this.price = price.ToString();
        }
    }

    [Serializable]
    public class WebsocketLimitOpenLongOrder : WebsocketLimitOrder
    {
        public WebsocketLimitOpenLongOrder(string symbol, double quantity, double price) : base(symbol, "BUY", "LONG", "LIMIT", quantity, "GTC", price)
        {
        }
    }

    [Serializable]
    public class WebsocketLimitOpenShortOrder : WebsocketLimitOrder
    {
        public WebsocketLimitOpenShortOrder(string symbol, double quantity, double price) : base(symbol, "SELL", "SHORT", "LIMIT", quantity, "GTC", price)
        {
        }
    }
    #endregion

    #region Conditional market order
    [Serializable]
    public class WebsocketConditionalMarketOrder : WebsocketOrder
    {
        public string timeInForce;
        public string stopPrice;

        public WebsocketConditionalMarketOrder(string symbol, string side, string positionSide, string type, double? quantity, string timeInForce, double stopPrice) : base(symbol, side, positionSide, type, quantity)
        {
            this.timeInForce = timeInForce;
            this.stopPrice = stopPrice.ToString();
        }
    }

    [Serializable]
    public class WebsocketConditionalMarketOpenLongOrder : WebsocketConditionalMarketOrder
    {
        public WebsocketConditionalMarketOpenLongOrder(string symbol, double quantity, double stopPrice) : base(symbol, "BUY", "LONG", "STOP_MARKET", quantity, "GTC", stopPrice)
        {
        }
    }

    [Serializable]
    public class WebsocketTakeProfitCloseLongOrder : WebsocketConditionalMarketOrder
    {
        public WebsocketTakeProfitCloseLongOrder(string symbol, double? quantity, double stopPrice) : base(symbol, "SELL", "LONG", "TAKE_PROFIT_MARKET", quantity, "GTC", stopPrice)
        {
        }
    }

    [Serializable]
    public class WebsocketStopLossCloseLongOrder : WebsocketConditionalMarketOrder
    {
        public WebsocketStopLossCloseLongOrder(string symbol, double? quantity, double stopPrice) : base(symbol, "SELL", "LONG", "STOP_MARKET", quantity, "GTC", stopPrice)
        {
        }
    }

    [Serializable]
    public class WebsocketConditionalMarketOpenShortOrder : WebsocketConditionalMarketOrder
    {
        public WebsocketConditionalMarketOpenShortOrder(string symbol, double quantity, double stopPrice) : base(symbol, "SELL", "SHORT", "STOP_MARKET", quantity, "GTC", stopPrice)
        {
        }
    }

    [Serializable]
    public class WebsocketTakeProfitCloseShortOrder : WebsocketConditionalMarketOrder
    {
        public WebsocketTakeProfitCloseShortOrder(string symbol, double? quantity, double stopPrice) : base(symbol, "BUY", "SHORT", "TAKE_PROFIT_MARKET", quantity, "GTC", stopPrice)
        {
        }
    }

    [Serializable]
    public class WebsocketStopLossCloseShortOrder : WebsocketConditionalMarketOrder
    {
        public WebsocketStopLossCloseShortOrder(string symbol, double? quantity, double stopPrice) : base(symbol, "BUY", "SHORT", "STOP_MARKET", quantity, "GTC", stopPrice)
        {
        }
    }
    #endregion

    #region Conditional limit order
    [Serializable]
    public class WebsocketConditionalLimitOrder : WebsocketOrder
    {
        public string timeInForce;
        public string stopPrice;
        public string price;

        public WebsocketConditionalLimitOrder(string symbol, string side, string positionSide, string type, double? quantity, string timeInForce, double stopPrice, double price) : base(symbol, side, positionSide, type, quantity)
        {
            this.timeInForce = timeInForce;
            this.stopPrice = stopPrice.ToString();
            this.price = price.ToString();
        }
    }

    [Serializable]
    public class WebsocketConditionalLimitOpenLongOrder : WebsocketConditionalLimitOrder
    {
        public WebsocketConditionalLimitOpenLongOrder(string symbol, double quantity, double stopPrice, double price) : base(symbol, "BUY", "LONG", "STOP", quantity, "GTC", stopPrice, price)
        {
        }
    }

    [Serializable]
    public class WebsocketConditionalLimitOpenShortOrder : WebsocketConditionalLimitOrder
    {
        public WebsocketConditionalLimitOpenShortOrder(string symbol, double quantity, double stopPrice, double price) : base(symbol, "SELL", "SHORT", "STOP", quantity, "GTC", stopPrice, price)
        {
        }
    }
    #endregion

    #region Trailing stop order
    [Serializable]
    public class WebsocketTrailingStopOrder : WebsocketOrder
    {
        public string timeInForce;
        public string callbackRate;
        public string activationPrice;

        public WebsocketTrailingStopOrder(string symbol, string side, string positionSide, string type, double? quantity, string timeInForce, double callbackRate, double? activationPrice) : base(symbol, side, positionSide, type, quantity)
        {
            this.timeInForce = timeInForce;
            this.callbackRate = callbackRate.ToString();
            this.activationPrice = activationPrice?.ToString();
        }
    }

    [Serializable]
    public class WebrequestTrailingStopCloseLongOrder : WebsocketTrailingStopOrder
    {
        public WebrequestTrailingStopCloseLongOrder(string symbol, double quantity, double callbackRate, double? activationPrice) : base(symbol, "SELL", "LONG", "TRAILING_STOP_MARKET", quantity, "GTC", callbackRate, activationPrice)
        {
        }
    }

    [Serializable]
    public class WebrequestTrailingStopCloseShortOrder : WebsocketTrailingStopOrder
    {
        public WebrequestTrailingStopCloseShortOrder(string symbol, double quantity, double callbackRate, double? activationPrice) : base(symbol, "BUY", "SHORT", "TRAILING_STOP_MARKET", quantity, "GTC", callbackRate, activationPrice)
        {
        }
    }
    #endregion
}