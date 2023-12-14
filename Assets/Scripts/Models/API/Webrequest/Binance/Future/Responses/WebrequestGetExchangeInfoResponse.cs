using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebrequestGetExchangeInfoResponse : WebrequestGeneralResponse
    {
        public string timezone;
        public long serverTime;
        public string futuresType;
        public List<WebrequestGetExchangeInfoResponseRateLimit> rateLimits;
        public List<object> exchangeFilters;
        public List<WebrequestGetExchangeInfoResponseAsset> assets;
        public List<WebrequestGetExchangeInfoResponseSymbol> symbols;
    }

    [Serializable]
    public class WebrequestGetExchangeInfoResponseRateLimit
    {
        public string rateLimitType;
        public string interval;
        public long intervalNum;
        public long limit;
    }

    [Serializable]
    public class WebrequestGetExchangeInfoResponseAsset
    {
        public string asset;
        public string marginAvailable;
        public string autoAssetExchange;
    }

    [Serializable]
    public class WebrequestGetExchangeInfoResponseSymbol
    {
        public string symbol;
        public string pair;
        public string contractType;
        public long deliveryDate;
        public long onboardDate;
        public string status;
        public string maintMarginPercent;
        public string requiredMarginPercent;
        public string baseAsset;
        public string quoteAsset;
        public string marginAsset;
        public long pricePrecision;
        public long quantityPrecision;
        public long baseAssetPrecision;
        public long quotePrecision;
        public string underlyingType;
        public List<object> underlyingSubType;
        public long settlePlan;
        public string triggerProtect;
        public string liquidationFee;
        public string marketTakeBound;
        public long maxMoveOrderLimit;
        public List<WebrequestGetExchangeInfoResponseSymbolFilter> filters;
        public List<string> orderTypes;
        public List<string> timeInForce;
    }

    [Serializable]
    public class WebrequestGetExchangeInfoResponseSymbolFilter
    {
        public string filterType;
        public string minPrice;
        public string maxPrice;
        public string tickSize;
        public string stepSize;
        public string maxQty;
        public string minQty;
        public string limit;
        public string notional;
        public string multiplierDown;
        public string multiplierUp;
        public string multiplierDecimal;
    }
}