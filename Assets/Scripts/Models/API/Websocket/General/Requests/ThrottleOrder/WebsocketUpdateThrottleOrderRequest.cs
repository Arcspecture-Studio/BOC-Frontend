using System;

namespace General
{
    [Serializable]
    public class WebsocketUpdateThrottleOrderRequest : WebsocketGeneralRequest
    {
        public WebsocketThrottleOrderRequest orderRequest;
        public WebsocketUpdateThrottleOrderRequest(string token,
            string orderId,
            OrderTypeEnum orderType) : base(WebsocketEventTypeEnum.UPDATE_THROTTLE_ORDER, token)
        {
            orderRequest = new(orderId, null, null, orderType);
        }
    }
}