using System;

namespace General
{
    [Serializable]
    public class WebsocketSubmitOrderRequest : WebsocketIdRequest
    {
        public WebsocketSubmitOrderRequest(string token, string orderId) : base(WebsocketEventTypeEnum.SUBMIT_ORDER, token, orderId)
        {
        }
    }
}