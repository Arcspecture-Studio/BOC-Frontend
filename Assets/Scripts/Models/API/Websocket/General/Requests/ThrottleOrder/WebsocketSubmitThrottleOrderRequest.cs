using System;

namespace General
{
    [Serializable]
    public class WebsocketSubmitThrottleOrderRequest : WebsocketIdRequest
    {
        public WebsocketSubmitThrottleOrderRequest(string token,
            string orderId) : base(WebsocketEventTypeEnum.SUBMIT_THROTTLE_ORDER, token, orderId)
        {
        }
    }
}