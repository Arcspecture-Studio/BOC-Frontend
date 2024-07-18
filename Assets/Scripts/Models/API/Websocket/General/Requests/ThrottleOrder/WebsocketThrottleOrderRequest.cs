#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

using System;

namespace General
{
    [Serializable]
    public class WebsocketThrottleOrderRequest : WebsocketIdRequest
    {
        public string? parentOrderId;
        public CalculateThrottle? throttleCalculator;
        public OrderTypeEnum? orderType;
        public TakeProfitTypeEnum breakEvenType;

        public WebsocketThrottleOrderRequest(
            WebsocketEventTypeEnum eventType,
            string token,
            string orderId,
            string? parentOrderId,
            CalculateThrottle? throttleCalculator,
            OrderTypeEnum? orderType,
            TakeProfitTypeEnum breakEvenType) : base(eventType, token, orderId)
        {
            this.parentOrderId = parentOrderId;
            this.throttleCalculator = throttleCalculator;
            this.orderType = orderType;
            this.breakEvenType = breakEvenType;
        }
    }
}