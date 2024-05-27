using System;

namespace General
{
    [Serializable]
    public class WebsocketSubmitOrderRequest : WebsocketIdRequest
    {
        public float quantityToClose;

        public WebsocketSubmitOrderRequest(string token, string orderId, float quantityToClose) : base(WebsocketEventTypeEnum.SUBMIT_ORDER, token, orderId)
        {
            this.quantityToClose = quantityToClose;
        }
    }
}