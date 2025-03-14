#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

using System;

namespace General
{
    [Serializable]
    public class WebsocketOrderRequest : WebsocketIdRequest
    {
        public string? symbol;
        public CalculateMargin? marginCalculator;
        public OrderTypeEnum? orderType;
        public string? tradingBotId;

        public WebsocketOrderRequest(WebsocketEventTypeEnum eventType,
            string token,
            string orderId,
            string? symbol,
            CalculateMargin? marginCalculator,
            OrderTypeEnum? orderType,
            string? tradingBotId) : base(eventType, token, orderId)
        {
            this.symbol = symbol;
            this.marginCalculator = marginCalculator;
            this.orderType = orderType;
            this.tradingBotId = tradingBotId;
        }
    }
}