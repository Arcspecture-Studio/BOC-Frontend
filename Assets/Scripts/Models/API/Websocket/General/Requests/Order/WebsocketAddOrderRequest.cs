using System;

namespace General
{
    [Serializable]
    public class WebsocketAddOrderRequest : WebsocketOrderRequest
    {
        public WebsocketAddOrderRequest(string token,
            string orderId,
            string symbol,
            CalculateMargin marginCalculator,
            TakeProfitTypeEnum takeProfitType,
            OrderTypeEnum orderType) :
            base(WebsocketEventTypeEnum.ADD_ORDER, token, orderId, symbol, marginCalculator, takeProfitType, orderType, null)
        {
        }
    }
}