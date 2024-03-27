using System;

namespace General
{
    [Serializable]
    public class WebsocketSubmitThrottleOrderResponse : WebsocketSubmitOrderResponse
    {
        public string parentOrderId;
    }
}