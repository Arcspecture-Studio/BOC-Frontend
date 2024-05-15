using System;

namespace General
{
    [Serializable]
    public class WebsocketDeleteOrderRequest : WebsocketIdRequest
    {
        public WebsocketDeleteOrderRequest(string token, string orderId) : base(WebsocketEventTypeEnum.DELETE_ORDER, token, orderId)
        {
        }
    }
}