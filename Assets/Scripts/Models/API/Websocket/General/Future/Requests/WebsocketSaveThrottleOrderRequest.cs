using System;

namespace General
{
    [Serializable]
    public class WebsocketSaveThrottleOrderRequest : WebsocketGeneralRequest
    {
        public WebsocketThrottleOrderRequest orderRequest;
        public WebsocketSaveOrderEnum actionToTake;

        public WebsocketSaveThrottleOrderRequest(Guid orderId, Guid parentOrderId, PlatformEnum platform, bool submit = false) : base(WebsocketEventTypeEnum.SAVE_THROTTLE_ORDER, platform)
        {
            orderRequest = new WebsocketThrottleOrderRequest(orderId, parentOrderId);
            if (submit) actionToTake = WebsocketSaveOrderEnum.SUBMIT;
            else actionToTake = WebsocketSaveOrderEnum.DELETE;
        }
        public WebsocketSaveThrottleOrderRequest(Guid orderId, Guid parentOrderId, PlatformEnum platform, OrderTypeEnum orderType) : base(WebsocketEventTypeEnum.SAVE_THROTTLE_ORDER, platform)
        {
            orderRequest = new WebsocketThrottleOrderRequest(orderId, parentOrderId, orderType);
            actionToTake = WebsocketSaveOrderEnum.UPDATE;
        }
        public WebsocketSaveThrottleOrderRequest(Guid orderId, Guid parentOrderId, PlatformEnum platform, CalculateThrottle throttlerCalculator, OrderTypeEnum orderType) : base(WebsocketEventTypeEnum.SAVE_THROTTLE_ORDER, platform)
        {
            orderRequest = new WebsocketThrottleOrderRequest(orderId, parentOrderId, platform, throttlerCalculator, orderType);
            actionToTake = WebsocketSaveOrderEnum.SAVE;
        }
    }
    [Serializable]
    public class WebsocketThrottleOrderRequest
    {
        public Guid orderId;
        public Guid parentOrderId;
        public CalculateThrottle throttleCalculator;
        public OrderTypeEnum orderType;

        public WebsocketThrottleOrderRequest(Guid orderId, Guid parentOrderId)
        {
            this.orderId = orderId;
            this.parentOrderId = parentOrderId;
        }
        public WebsocketThrottleOrderRequest(Guid orderId, Guid parentOrderId, OrderTypeEnum orderType)
        {
            this.orderId = orderId;
            this.parentOrderId = parentOrderId;
            this.orderType = orderType;
        }
        public WebsocketThrottleOrderRequest(Guid orderId, Guid parentOrderId,
            PlatformEnum platform, CalculateThrottle throttleCalculator, OrderTypeEnum orderType)
        {
            this.orderId = orderId;
            this.parentOrderId = parentOrderId;
            this.throttleCalculator = throttleCalculator;
            this.orderType = orderType;
        }
    }
}