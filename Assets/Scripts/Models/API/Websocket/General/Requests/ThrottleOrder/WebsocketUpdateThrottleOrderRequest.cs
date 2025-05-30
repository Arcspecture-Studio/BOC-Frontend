using System;

namespace General
{
    [Serializable]
    public class WebsocketUpdateThrottleOrderRequest : WebsocketThrottleOrderRequest
    {
        public bool disableExit;
        public WebsocketUpdateThrottleOrderRequest(string token,
            string orderId,
            OrderTypeEnum orderType,
            TakeProfitTypeEnum breakEvenType,
            bool disableExit) : base(WebsocketEventTypeEnum.UPDATE_THROTTLE_ORDER,
            token, orderId, null, null, orderType, breakEvenType)
        {
            this.disableExit = disableExit;
        }
    }
}