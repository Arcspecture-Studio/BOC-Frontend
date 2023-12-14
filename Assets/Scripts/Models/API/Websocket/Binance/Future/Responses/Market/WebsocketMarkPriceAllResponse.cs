using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebsocketMarkPriceAllResponse : WebsocketMarketResponseData<List<WebsocketMarkPriceData>>
    {
    }
}