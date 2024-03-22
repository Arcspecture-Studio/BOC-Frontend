using System;

namespace General
{
    [Serializable]
    public class WebsocketSubmitOrderRequest : WebsocketGeneralRequest
    {
        public WebsocketOrderIdRequest orderRequest;

        public WebsocketSubmitOrderRequest(string token, string orderId) : base(WebsocketEventTypeEnum.SUBMIT_ORDER, token)
        {
            orderRequest = new(orderId);
        }
    }
}