using System;

namespace General
{
    [Serializable]
    public class WebsocketSaveOrderRequest : WebsocketGeneralRequest
    {
        public WebsocketOrderIdRequest orderRequest;
        public WebsocketDataActionEnum actionToTake;

        public WebsocketSaveOrderRequest(string orderId, PlatformEnum platform, bool submit = false) : base(WebsocketEventTypeEnum.SAVE_ORDER, platform)
        {
            if (submit) actionToTake = WebsocketDataActionEnum.SUBMIT;
            else actionToTake = WebsocketDataActionEnum.DELETE;
            orderRequest = new WebsocketOrderIdRequest(orderId);
        }
        public WebsocketSaveOrderRequest(string orderId,
            PlatformEnum platform,
            CalculateMargin marginCalculator,
            OrderTakeProfitTypeEnum takeProfitType,
            OrderTypeEnum orderType) : base(WebsocketEventTypeEnum.SAVE_ORDER, platform)
        {
            actionToTake = WebsocketDataActionEnum.UPDATE;
            orderRequest = new WebsocketOrderRequest(orderId, marginCalculator, takeProfitType, orderType);
        }
        public WebsocketSaveOrderRequest(string orderId,
            PlatformEnum platform,
            CalculateMargin marginCalculator,
            string symbol,
            OrderTakeProfitTypeEnum takeProfitType,
            OrderTypeEnum orderType) : base(WebsocketEventTypeEnum.SAVE_ORDER, platform)
        {
            actionToTake = WebsocketDataActionEnum.SAVE;
            orderRequest = new WebsocketOrderRequest(orderId, marginCalculator, symbol, takeProfitType, orderType);
        }
    }
    [Serializable]
    public class WebsocketOrderRequest : WebsocketOrderIdRequest
    {
        public string symbol;
        public CalculateMargin marginCalculator;
        public OrderTakeProfitTypeEnum takeProfitType;
        public OrderTypeEnum orderType;

        public WebsocketOrderRequest(string orderId,
            CalculateMargin marginCalculator,
            OrderTakeProfitTypeEnum takeProfitType,
            OrderTypeEnum orderType) : base(orderId)
        {
            this.marginCalculator = marginCalculator;
            this.takeProfitType = takeProfitType;
            this.orderType = orderType;
        }
        public WebsocketOrderRequest(string orderId,
            CalculateMargin marginCalculator,
            string symbol,
            OrderTakeProfitTypeEnum takeProfitType,
            OrderTypeEnum orderType) : base(orderId)
        {
            this.symbol = symbol;
            this.marginCalculator = marginCalculator;
            this.takeProfitType = takeProfitType;
            this.orderType = orderType;
        }
    }
    [Serializable]
    public class WebsocketOrderIdRequest
    {
        public string orderId;
        public WebsocketOrderIdRequest(string orderId)
        {
            this.orderId = orderId;
        }
    }
}