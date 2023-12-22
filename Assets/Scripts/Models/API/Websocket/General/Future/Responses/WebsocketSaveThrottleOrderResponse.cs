using System;

namespace General
{
    [Serializable]
    public class WebsocketSaveThrottleOrderResponse : WebsocketSaveOrderResponse
    {
        public string parentOrderId;
    }
}