using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebsocketTickerMiniAllResponse : WebsocketMarketResponseData<List<WebsocketTickerMiniData>>
    {
    }
}