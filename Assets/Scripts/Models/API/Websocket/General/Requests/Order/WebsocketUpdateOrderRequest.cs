using System;

namespace General
{
    [Serializable]
    public class WebsocketUpdateOrderRequest : WebsocketOrderRequest
    {
        public WebsocketUpdateOrderRequest(string token,
            string orderId,
            CalculateMargin marginCalculator,
            TakeProfitTypeEnum takeProfitType,
            OrderTypeEnum orderType,
            string tradingBotId) : base(WebsocketEventTypeEnum.UPDATE_ORDER, token, orderId, null, marginCalculator, takeProfitType, orderType, tradingBotId)
        {
        }
    }
}