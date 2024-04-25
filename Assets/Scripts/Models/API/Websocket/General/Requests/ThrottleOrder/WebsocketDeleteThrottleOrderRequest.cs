using System;

namespace General
{
    [Serializable]
    public class WebsocketDeleteThrottleOrderRequest : WebsocketIdRequest
    {
        public WebsocketDeleteThrottleOrderRequest(string token,
            string orderId) : base(WebsocketEventTypeEnum.DELETE_THROTTLE_ORDER, token, orderId)
        {
        }
    }
}