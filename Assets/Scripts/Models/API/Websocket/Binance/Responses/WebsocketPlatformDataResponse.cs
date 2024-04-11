using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebsocketPlatformDataResponse
    {
        public Dictionary<string, WebrequestGetExchangeInfoResponseSymbol> exchangeInfos;
        public Dictionary<string, WebrequestGetBalanceResponseData> balances;
    }
}