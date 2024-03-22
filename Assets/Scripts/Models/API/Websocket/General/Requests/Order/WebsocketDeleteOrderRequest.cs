using System;

namespace General
{
    [Serializable]
    public class WebsocketDeleteOrderRequest : WebsocketGeneralRequest
    {
        public WebsocketOrderIdRequest orderRequest;

        public WebsocketDeleteOrderRequest(string token, string orderId) : base(WebsocketEventTypeEnum.DELETE_ORDER, token)
        {
            orderRequest = new(orderId);
        }
    }
}