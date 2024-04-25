using System;

namespace General
{
    [Serializable]
    public class WebsocketQuickOrderRequest : WebsocketGeneralRequest
    {
        public double entryPrice; // -1 means null
        public bool isLong;

        public WebsocketQuickOrderRequest(WebsocketEventTypeEnum eventType, string token, double entryPrice, bool isLong) : base(eventType, token)
        {
            this.entryPrice = entryPrice;
            this.isLong = isLong;
        }
    }
}