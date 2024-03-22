using System;

namespace General
{
    [Serializable]
    public class WebsocketUpdateOrderRequest : WebsocketGeneralRequest
    {
        public WebsocketOrderRequest orderRequest;
        public WebsocketUpdateOrderRequest(string token,
            string orderId,
            CalculateMargin marginCalculator,
            TakeProfitTypeEnum takeProfitType,
            OrderTypeEnum orderType) : base(WebsocketEventTypeEnum.ADD_ORDER, token)
        {
            orderRequest = new(orderId, null, null, marginCalculator, takeProfitType, orderType);
        }
    }
}