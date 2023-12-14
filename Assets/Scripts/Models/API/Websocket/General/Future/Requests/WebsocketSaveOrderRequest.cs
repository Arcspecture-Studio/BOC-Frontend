using System;
using System.Collections.Generic;

namespace General
{
    [Serializable]
    public class WebsocketSaveOrderRequest : WebsocketGeneralRequest
    {
        public WebsocketOrderRequest orderRequest;
        public WebsocketSaveOrderEnum actionToTake;

        public WebsocketSaveOrderRequest(Guid orderId, PlatformEnum platform, bool submit = false) : base(WebsocketEventTypeEnum.SAVE_ORDER, platform)
        {
            orderRequest = new WebsocketOrderRequest(orderId);
            if (submit) actionToTake = WebsocketSaveOrderEnum.SUBMIT;
            else actionToTake = WebsocketSaveOrderEnum.DELETE;
        }
        public WebsocketSaveOrderRequest(Guid orderId,
            PlatformEnum platform,
            CalculateMargin marginCalculator,
            OrderTakeProfitTypeEnum takeProfitType,
            OrderTypeEnum orderType) : base(WebsocketEventTypeEnum.SAVE_ORDER, platform)
        {
            orderRequest = new WebsocketOrderRequest(orderId, marginCalculator, takeProfitType, orderType);
            actionToTake = WebsocketSaveOrderEnum.UPDATE;
        }
        public WebsocketSaveOrderRequest(Guid orderId,
            PlatformEnum platform,
            CalculateMargin marginCalculator,
            string symbol,
            OrderTakeProfitTypeEnum takeProfitType,
            OrderTypeEnum orderType) : base(WebsocketEventTypeEnum.SAVE_ORDER, platform)
        {
            orderRequest = new WebsocketOrderRequest(orderId, marginCalculator, symbol, takeProfitType, orderType);
            actionToTake = WebsocketSaveOrderEnum.SAVE;
        }
    }
    [Serializable]
    public class WebsocketOrderRequest
    {
        public Guid orderId;
        public string symbol;
        public CalculateMargin marginCalculator;
        public OrderTakeProfitTypeEnum takeProfitType;
        public OrderTypeEnum orderType;

        public WebsocketOrderRequest(Guid orderId)
        {
            this.orderId = orderId;
        }
        public WebsocketOrderRequest(Guid orderId,
            CalculateMargin marginCalculator,
            OrderTakeProfitTypeEnum takeProfitType,
            OrderTypeEnum orderType)
        {
            this.orderId = orderId;
            this.marginCalculator = marginCalculator;
            this.takeProfitType = takeProfitType;
            this.orderType = orderType;
        }
        public WebsocketOrderRequest(Guid orderId,
            CalculateMargin marginCalculator,
            string symbol,
            OrderTakeProfitTypeEnum takeProfitType,
            OrderTypeEnum orderType)
        {
            this.orderId = orderId;
            this.symbol = symbol;
            this.marginCalculator = marginCalculator;
            this.takeProfitType = takeProfitType;
            this.orderType = orderType;
        }
    }
}