using System;

namespace General
{
    [Serializable]
    public class WebsocketAddOrderRequest : WebsocketOrderRequest
    {
        public WebsocketAddOrderRequest(string token,
            string orderId,
            string symbol,
            MarginCalculatorAdd marginCalculator,
            OrderTypeEnum orderType) :
            base(WebsocketEventTypeEnum.ADD_ORDER, token, orderId, symbol, marginCalculator, orderType, null)
        {
        }
    }
}