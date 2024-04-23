using System;

namespace General
{
    [Serializable]
    public class WebsocketQuickOrderRequest
    {
        public double entryPrice; // -1 means null
        public bool isLong;

        public WebsocketQuickOrderRequest(double entryPrice, bool isLong)
        {
            this.entryPrice = entryPrice;
            this.isLong = isLong;
        }
    }
}