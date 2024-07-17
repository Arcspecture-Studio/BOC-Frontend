using System;

namespace General
{
    [Serializable]
    public class WebsocketUpdateThrottleOrderRequest : WebsocketThrottleOrderRequest
    {
        public WebsocketUpdateThrottleOrderRequest(string token,
            string orderId,
            OrderTypeEnum orderType,
            TakeProfitTypeEnum breakEvenType) : base(WebsocketEventTypeEnum.UPDATE_THROTTLE_ORDER,
            token, orderId, null, null, orderType, breakEvenType)
        {
        }
    }
}