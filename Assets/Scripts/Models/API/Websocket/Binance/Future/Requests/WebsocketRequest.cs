using System;

namespace Binance
{
    [Serializable]
    public class WebsocketRequest
    {
        public long id;
        public string method;

        public WebsocketRequest(string method)
        {
            this.id = Utils.CurrentTimestamp();
            this.method = method;
        }
    }
}