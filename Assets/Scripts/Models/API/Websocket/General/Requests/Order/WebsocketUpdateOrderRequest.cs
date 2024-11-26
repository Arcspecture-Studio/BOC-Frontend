using System;

namespace General
{
    [Serializable]
    public class WebsocketUpdateOrderRequest : WebsocketOrderRequest
    {
        public WebsocketUpdateOrderRequest(string token,
            string orderId,
            MarginCalculatorUpdate marginCalculator,
            OrderTypeEnum orderType,
            string tradingBotId) : base(WebsocketEventTypeEnum.UPDATE_ORDER, token, orderId, null, marginCalculator, orderType, tradingBotId)
        {
        }
    }
}