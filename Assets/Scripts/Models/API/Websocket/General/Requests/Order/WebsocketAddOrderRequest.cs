using System;

namespace General
{
    [Serializable]
    public class WebsocketAddOrderRequest : WebsocketGeneralRequest
    {
        public WebsocketOrderRequest orderRequest;
        public WebsocketAddOrderRequest(string token,
            string orderId,
            PlatformEnum platform,
            string symbol,
            CalculateMargin marginCalculator,
            TakeProfitTypeEnum takeProfitType,
            OrderTypeEnum orderType) : base(WebsocketEventTypeEnum.ADD_ORDER, token)
        {
            orderRequest = new(orderId, platform, symbol, marginCalculator, takeProfitType, orderType, null);
        }
    }
}