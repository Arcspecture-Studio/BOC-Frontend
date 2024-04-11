using System;

namespace General
{
    [Serializable]
    public class WebsocketSubmitThrottleOrderRequest : WebsocketGeneralRequest
    {
        public WebsocketIdRequest orderRequest;
        public WebsocketSubmitThrottleOrderRequest(string token,
            string orderId) : base(WebsocketEventTypeEnum.SUBMIT_THROTTLE_ORDER, token)
        {
            orderRequest = new(orderId);
        }
    }
}