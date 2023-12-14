using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebsocketTickerAllResponse : WebsocketMarketResponseData<List<WebsocketTickerData>>
    {
    }
}