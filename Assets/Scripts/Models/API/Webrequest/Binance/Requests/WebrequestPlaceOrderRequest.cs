using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Specialized;
using System.Web;

namespace Binance
{
    [Serializable]
    public class WebrequestPlaceOrderRequest : WebrequestRequest
    {
        public string jsonString; // Used in WebrequestPlaceMultipleOrdersRequest

        public WebrequestPlaceOrderRequest(bool testnet) : base(testnet)
        {
            path = "/fapi/v1/order";
            requestType = WebrequestRequestTypeEnum.POST;
        }
        protected void QueriesToJsonString(string queries)
        {
            NameValueCollection queryParameters = HttpUtility.ParseQueryString(queries);
            JObject jsonObject = new JObject();
            foreach (string key in queryParameters.AllKeys) jsonObject.Add(key, queryParameters[key]);
            jsonString = JsonConvert.SerializeObject(jsonObject, JsonSerializerConfig.settings);
        }
    }

    #region Market order
    [Serializable]
    public class WebrequestMarketOrderRequest : WebrequestPlaceOrderRequest
    {
        public WebrequestMarketOrderRequest(bool testnet, string apiSecret,
            string symbol,
            string side,
            string positionSide,
            string type,
            double quantity) : base(testnet)
        {
            string queries =
                "symbol=" + symbol +
                "&side=" + side +
                "&positionSide=" + positionSide +
                "&type=" + type +
                "&quantity=" + quantity.ToString();
            QueriesToJsonString(queries);
            UpdateUri(queries, apiSecret);
        }
    }

    [Serializable]
    public class WebrequestMarketOpenLongRequest : WebrequestMarketOrderRequest
    {
        public WebrequestMarketOpenLongRequest(bool testnet, string apiSecret, string symbol, double quantity) : base(testnet, apiSecret, symbol, "BUY", "LONG", "MARKET", quantity)
        {
        }
    }

    [Serializable]
    public class WebrequestMarketCloseLongRequest : WebrequestMarketOrderRequest
    {
        public WebrequestMarketCloseLongRequest(bool testnet, string apiSecret, string symbol, double quantity) : base(testnet, apiSecret, symbol, "SELL", "LONG", "MARKET", quantity)
        {
        }
    }

    [Serializable]
    public class WebrequestMarketOpenShortRequest : WebrequestMarketOrderRequest
    {
        public WebrequestMarketOpenShortRequest(bool testnet, string apiSecret, string symbol, double quantity) : base(testnet, apiSecret, symbol, "SELL", "SHORT", "MARKET", quantity)
        {
        }
    }

    [Serializable]
    public class WebrequestMarketCloseShortRequest : WebrequestMarketOrderRequest
    {
        public WebrequestMarketCloseShortRequest(bool testnet, string apiSecret, string symbol, double quantity) : base(testnet, apiSecret, symbol, "BUY", "SHORT", "MARKET", quantity)
        {
        }
    }
    #endregion

    #region Limit order
    [Serializable]
    public class WebrequestLimitOrderRequest : WebrequestPlaceOrderRequest
    {
        public WebrequestLimitOrderRequest(bool testnet, string apiSecret,
            string symbol,
            string side,
            string positionSide,
            string type,
            string timeInForce,
            double quantity,
            double price) : base(testnet)
        {
            string queries =
                "symbol=" + symbol +
                "&side=" + side +
                "&positionSide=" + positionSide +
                "&type=" + type +
                "&timeInForce=" + timeInForce +
                "&quantity=" + quantity.ToString() +
                "&price=" + price.ToString();
            QueriesToJsonString(queries);
            UpdateUri(queries, apiSecret);
        }
    }

    [Serializable]
    public class WebrequestLimitOpenLongRequest : WebrequestLimitOrderRequest
    {
        public WebrequestLimitOpenLongRequest(bool testnet, string apiSecret, string symbol, double quantity, double price) : base(testnet, apiSecret, symbol, "BUY", "LONG", "LIMIT", "GTC", quantity, price)
        {
        }
    }

    [Serializable]
    public class WebrequestLimitOpenShortRequest : WebrequestLimitOrderRequest
    {
        public WebrequestLimitOpenShortRequest(bool testnet, string apiSecret, string symbol, double quantity, double price) : base(testnet, apiSecret, symbol, "SELL", "SHORT", "LIMIT", "GTC", quantity, price)
        {
        }
    }
    #endregion

    #region Conditional market order
    [Serializable]
    public class WebrequestConditionalMarketOrderRequest : WebrequestPlaceOrderRequest
    {
        public WebrequestConditionalMarketOrderRequest(bool testnet, string apiSecret,
            string symbol,
            string side,
            string positionSide,
            string type,
            string timeInForce,
            double? quantity,
            double stopPrice) : base(testnet)
        {
            string closePositionQuery;
            if (!quantity.HasValue) closePositionQuery = "&closePosition=true";
            else closePositionQuery = "&quantity=" + quantity.ToString();
            string queries =
                "symbol=" + symbol +
                "&side=" + side +
                "&positionSide=" + positionSide +
                "&type=" + type +
                "&timeInForce=" + timeInForce +
                closePositionQuery +
                "&stopPrice=" + stopPrice.ToString();
            QueriesToJsonString(queries);
            UpdateUri(queries, apiSecret);
        }
    }

    [Serializable]
    public class WebrequestConditionalMarketOpenLongRequest : WebrequestConditionalMarketOrderRequest
    {
        public WebrequestConditionalMarketOpenLongRequest(bool testnet, string apiSecret, string symbol, double quantity, double stopPrice) : base(testnet, apiSecret, symbol, "BUY", "LONG", "STOP_MARKET", "GTC", quantity, stopPrice)
        {
        }
    }

    [Serializable]
    public class WebrequestTakeProfitCloseLongRequest : WebrequestConditionalMarketOrderRequest
    {
        public WebrequestTakeProfitCloseLongRequest(bool testnet, string apiSecret, string symbol, double? quantity, double stopPrice) : base(testnet, apiSecret, symbol, "SELL", "LONG", "TAKE_PROFIT_MARKET", "GTC", quantity, stopPrice)
        {
        }
    }

    [Serializable]
    public class WebrequestStopLossCloseLongRequest : WebrequestConditionalMarketOrderRequest
    {
        public WebrequestStopLossCloseLongRequest(bool testnet, string apiSecret, string symbol, double? quantity, double stopPrice) : base(testnet, apiSecret, symbol, "SELL", "LONG", "STOP_MARKET", "GTC", quantity, stopPrice)
        {
        }
    }

    [Serializable]
    public class WebrequestConditionalMarketOpenShortRequest : WebrequestConditionalMarketOrderRequest
    {
        public WebrequestConditionalMarketOpenShortRequest(bool testnet, string apiSecret, string symbol, double quantity, double stopPrice) : base(testnet, apiSecret, symbol, "SELL", "SHORT", "STOP_MARKET", "GTC", quantity, stopPrice)
        {
        }
    }

    [Serializable]
    public class WebrequestTakeProfitCloseShortRequest : WebrequestConditionalMarketOrderRequest
    {
        public WebrequestTakeProfitCloseShortRequest(bool testnet, string apiSecret, string symbol, double? quantity, double stopPrice) : base(testnet, apiSecret, symbol, "BUY", "SHORT", "TAKE_PROFIT_MARKET", "GTC", quantity, stopPrice)
        {
        }
    }

    [Serializable]
    public class WebrequestStopLossCloseShortRequest : WebrequestConditionalMarketOrderRequest
    {
        public WebrequestStopLossCloseShortRequest(bool testnet, string apiSecret, string symbol, double? quantity, double stopPrice) : base(testnet, apiSecret, symbol, "BUY", "SHORT", "STOP_MARKET", "GTC", quantity, stopPrice)
        {
        }
    }
    #endregion

    #region Conditional limit order
    [Serializable]
    public class WebrequestConditionalLimitOrderRequest : WebrequestPlaceOrderRequest
    {
        public WebrequestConditionalLimitOrderRequest(bool testnet, string apiSecret,
            string symbol,
            string side,
            string positionSide,
            string type,
            string timeInForce,
            double quantity,
            double stopPrice,
            double price) : base(testnet)
        {
            string queries =
                "symbol=" + symbol +
                "&side=" + side +
                "&positionSide=" + positionSide +
                "&type=" + type +
                "&timeInForce=" + timeInForce +
                "&quantity=" + quantity.ToString() +
                "&stopPrice=" + stopPrice.ToString() +
                "&price=" + price.ToString();
            QueriesToJsonString(queries);
            UpdateUri(queries, apiSecret);
        }
    }

    [Serializable]
    public class WebrequestConditionalLimitOpenLongRequest : WebrequestConditionalLimitOrderRequest
    {
        public WebrequestConditionalLimitOpenLongRequest(bool testnet, string apiSecret, string symbol, double quantity, double stopPrice, double price) : base(testnet, apiSecret, symbol, "BUY", "LONG", "STOP", "GTC", quantity, stopPrice, price)
        {
        }
    }

    [Serializable]
    public class WebrequestConditionalLimitOpenShortRequest : WebrequestConditionalLimitOrderRequest
    {
        public WebrequestConditionalLimitOpenShortRequest(bool testnet, string apiSecret, string symbol, double quantity, double stopPrice, double price) : base(testnet, apiSecret, symbol, "SELL", "SHORT", "STOP", "GTC", quantity, stopPrice, price)
        {
        }
    }
    #endregion

    #region Trailing stop
    [Serializable]
    public class WebrequestTrailingStopOrderRequest : WebrequestPlaceOrderRequest
    {
        public WebrequestTrailingStopOrderRequest(bool testnet, string apiSecret,
            string symbol,
            string side,
            string positionSide,
            string type,
            string timeInForce,
            double quantity,
            double callbackRate,
            double? activationPrice) : base(testnet)
        {
            string activationPriceQuery;
            if (!activationPrice.HasValue) activationPriceQuery = "";
            else activationPriceQuery = "&activationPrice=" + activationPrice.ToString();
            string queries =
                "symbol=" + symbol +
                "&side=" + side +
                "&positionSide=" + positionSide +
                "&type=" + type +
                "&timeInForce=" + timeInForce +
                "&quantity=" + quantity.ToString() +
                "&callbackRate=" + callbackRate.ToString() +
                activationPriceQuery;
            QueriesToJsonString(queries);
            UpdateUri(queries, apiSecret);
        }
    }

    [Serializable]
    public class WebrequestTrailingStopCloseLongRequest : WebrequestTrailingStopOrderRequest
    {
        public WebrequestTrailingStopCloseLongRequest(bool testnet, string apiSecret, string symbol, double quantity, double callbackRate, double? activationPrice) : base(testnet, apiSecret, symbol, "SELL", "LONG", "TRAILING_STOP_MARKET", "GTC", quantity, callbackRate, activationPrice)
        {
        }
    }

    [Serializable]
    public class WebrequestTrailingStopCloseShortRequest : WebrequestTrailingStopOrderRequest
    {
        public WebrequestTrailingStopCloseShortRequest(bool testnet, string apiSecret, string symbol, double quantity, double callbackRate, double? activationPrice) : base(testnet, apiSecret, symbol, "BUY", "SHORT", "TRAILING_STOP_MARKET", "GTC", quantity, callbackRate, activationPrice)
        {
        }
    }
    #endregion
}