using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class ExchangeInfoResponse : ResponseWrapper<ExchangeInfoResult>
    {
    }

    [Serializable]
    public class ExchangeInfoResult
    {
        public string timezone;
        public long serverTime;
        public List<RateLimit> rateLimits;
        //public List<?> exchangeFilters;
        public List<ExchangeInfoResultSymbol> symbols;
    }

    [Serializable]
    public class ExchangeInfoResultSymbol
    {
        public string symbol;
        public string status;
        public string baseAsset;
        public long baseAssetPrecision;
        public string quoteAsset;
        public long quotePrecision;
        public long quoteAssetPrecision;
        public long baseCommissionPrecision;
        public long quoteCommissionPrecision;
        public List<string> orderTypes;
        public bool icebergAllowed;
        public bool ocoAllowed;
        public bool quoteOrderQtyMarketAllowed;
        public bool allowTrailingStop;
        public bool cancelReplaceAllowed;
        public bool isSpotTradingAllowed;
        public bool isMarginTradingAllowed;
        //public List<ExchangeInfoResultSymbolFilter> filters;
        public List<string> permissions;
        public string defaultSelfTradePreventionMode;
        public List<string> allowedSelfTradePreventionModes;
    }
}