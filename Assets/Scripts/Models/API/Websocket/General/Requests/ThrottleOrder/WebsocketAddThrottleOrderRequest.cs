using System;

namespace General
{
    [Serializable]
    public class WebsocketAddThrottleOrderRequest : WebsocketGeneralRequest
    {
        public WebsocketThrottleOrderRequest orderRequest;
        public WebsocketAddThrottleOrderRequest(string token,
            string orderId,
            string parentOrderId,
            CalculateThrottle throttleCalculator,
            OrderTypeEnum orderType) : base(WebsocketEventTypeEnum.ADD_THROTTLE_ORDER, token)
        {
            orderRequest = new(orderId, parentOrderId,
            throttleCalculator, orderType);
        }
    }
}