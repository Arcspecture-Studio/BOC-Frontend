#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

using System;

namespace General
{
    [Serializable]
    public class WebsocketOrderRequest : WebsocketOrderIdRequest
    {
        public PlatformEnum? platform;
        public string? symbol;
        public CalculateMargin? marginCalculator;
        public TakeProfitTypeEnum? takeProfitType;
        public OrderTypeEnum? orderType;

        public WebsocketOrderRequest(string orderId,
            PlatformEnum? platform,
            string? symbol,
            CalculateMargin? marginCalculator,
            TakeProfitTypeEnum? takeProfitType,
            OrderTypeEnum? orderType) : base(orderId)
        {
            this.platform = platform;
            this.symbol = symbol;
            this.marginCalculator = marginCalculator;
            this.takeProfitType = takeProfitType;
            this.orderType = orderType;
        }
    }
    [Serializable]
    public class WebsocketOrderIdRequest
    {
        public string _id;
        public WebsocketOrderIdRequest(string orderId)
        {
            this._id = orderId;
        }
    }
}