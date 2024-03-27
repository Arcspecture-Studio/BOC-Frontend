using System;

namespace General
{
    [Serializable]
    public class WebsocketDeleteThrottleOrderRequest : WebsocketGeneralRequest
    {
        public WebsocketOrderIdRequest orderRequest;
        public WebsocketDeleteThrottleOrderRequest(string token,
            string orderId) : base(WebsocketEventTypeEnum.DELETE_THROTTLE_ORDER, token)
        {
            orderRequest = new(orderId);
        }
    }
}