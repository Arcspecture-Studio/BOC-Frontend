#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

using System;

namespace General
{
    [Serializable]
    public class WebsocketOrderRequest : WebsocketIdRequest
    {
        public PlatformEnum? platform;
        public string? symbol;
        public CalculateMargin? marginCalculator;
        public TakeProfitTypeEnum? takeProfitType;
        public OrderTypeEnum? orderType;
        public string? tradingBotId;

        public WebsocketOrderRequest(WebsocketEventTypeEnum eventType,
            string token,
            string orderId,
            PlatformEnum? platform,
            string? symbol,
            CalculateMargin? marginCalculator,
            TakeProfitTypeEnum? takeProfitType,
            OrderTypeEnum? orderType,
            string? tradingBotId) : base(eventType, token, orderId)
        {
            this.platform = platform;
            this.symbol = symbol;
            this.marginCalculator = marginCalculator;
            this.takeProfitType = takeProfitType;
            this.orderType = orderType;
            this.tradingBotId = tradingBotId;
        }
    }
}