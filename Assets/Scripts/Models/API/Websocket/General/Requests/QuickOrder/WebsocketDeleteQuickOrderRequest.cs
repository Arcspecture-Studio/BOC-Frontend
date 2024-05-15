using System;

namespace General
{
    [Serializable]
    public class WebsocketDeleteQuickOrderRequest : WebsocketIdRequest
    {
        public WebsocketDeleteQuickOrderRequest(string token, string orderId) :
        base(WebsocketEventTypeEnum.DELETE_QUICK_ORDER, token, orderId)
        {
        }
    }
}