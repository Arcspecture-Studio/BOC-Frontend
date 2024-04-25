using System;

namespace General
{
    [Serializable]
    public class WebsocketAddThrottleOrderRequest : WebsocketThrottleOrderRequest
    {
        public WebsocketAddThrottleOrderRequest(string token,
            string orderId,
            string parentOrderId,
            CalculateThrottle throttleCalculator,
            OrderTypeEnum orderType) : base(WebsocketEventTypeEnum.ADD_THROTTLE_ORDER, token, orderId, parentOrderId, throttleCalculator, orderType)
        {
        }
    }
}