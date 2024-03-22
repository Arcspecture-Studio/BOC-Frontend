using System;

namespace General
{
    [Serializable]
    public class WebsocketSaveThrottleOrderResponse : WebsocketSubmitOrderResponse
    {
        public string parentOrderId;
    }
}