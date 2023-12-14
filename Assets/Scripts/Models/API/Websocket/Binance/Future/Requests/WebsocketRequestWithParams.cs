using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebsocketRequestWithParams : WebsocketRequest
    {
        public List<object> @params;

        public WebsocketRequestWithParams(string method, List<object> param) : base(method)
        {
            @params = param;
        }
    }
}