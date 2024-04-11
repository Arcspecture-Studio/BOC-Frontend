using System;

namespace General
{
    [Serializable]
    public class WebsocketDeleteQuickOrderResponse : WebsocketGeneralResponse
    {
        public string orderId;
    }
}