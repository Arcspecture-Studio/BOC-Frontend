#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

using System;

namespace General
{
    [Serializable]
    public class WebsocketOrderRequest : WebsocketIdRequest
    {
        public string? symbol;
        public MarginCalculatorUpdate? marginCalculator;
        public OrderTypeEnum? orderType;
        public FundingFeeHandlerEnum? fundingFeeHandler;
        public string? tradingBotId;

        public WebsocketOrderRequest(WebsocketEventTypeEnum eventType,
            string token,
            string orderId,
            string? symbol,
            MarginCalculatorUpdate? marginCalculator,
            OrderTypeEnum? orderType,
            FundingFeeHandlerEnum? fundingFeeHandler,
            string? tradingBotId) : base(eventType, token, orderId)
        {
            this.symbol = symbol;
            this.marginCalculator = marginCalculator;
            this.orderType = orderType;
            this.fundingFeeHandler = fundingFeeHandler;
            this.tradingBotId = tradingBotId;
        }
    }
}