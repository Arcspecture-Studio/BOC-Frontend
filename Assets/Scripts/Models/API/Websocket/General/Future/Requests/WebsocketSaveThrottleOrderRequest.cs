using System;

namespace General
{
    [Serializable]
    public class WebsocketSaveThrottleOrderRequest : WebsocketGeneralRequest
    {
        public WebsocketThrottleOrderRequest orderRequest;
        public WebsocketSaveOrderEnum actionToTake;

        public WebsocketSaveThrottleOrderRequest(string orderId, string parentOrderId, PlatformEnum platform, bool submit = false) : base(WebsocketEventTypeEnum.SAVE_THROTTLE_ORDER, platform)
        {
            orderRequest = new WebsocketThrottleOrderRequest(orderId, parentOrderId);
            if (submit) actionToTake = WebsocketSaveOrderEnum.SUBMIT;
            else actionToTake = WebsocketSaveOrderEnum.DELETE;
        }
        public WebsocketSaveThrottleOrderRequest(string orderId, string parentOrderId, PlatformEnum platform, OrderTypeEnum orderType) : base(WebsocketEventTypeEnum.SAVE_THROTTLE_ORDER, platform)
        {
            orderRequest = new WebsocketThrottleOrderRequest(orderId, parentOrderId, orderType);
            actionToTake = WebsocketSaveOrderEnum.UPDATE;
        }
        public WebsocketSaveThrottleOrderRequest(string orderId, string parentOrderId, PlatformEnum platform, CalculateThrottle throttlerCalculator, OrderTypeEnum orderType) : base(WebsocketEventTypeEnum.SAVE_THROTTLE_ORDER, platform)
        {
            orderRequest = new WebsocketThrottleOrderRequest(orderId, parentOrderId, platform, throttlerCalculator, orderType);
            actionToTake = WebsocketSaveOrderEnum.SAVE;
        }
    }
    [Serializable]
    public class WebsocketThrottleOrderRequest
    {
        public string orderId;
        public string parentOrderId;
        public CalculateThrottle throttleCalculator;
        public OrderTypeEnum orderType;

        public WebsocketThrottleOrderRequest(string orderId, string parentOrderId)
        {
            this.orderId = orderId;
            this.parentOrderId = parentOrderId;
        }
        public WebsocketThrottleOrderRequest(string orderId, string parentOrderId, OrderTypeEnum orderType)
        {
            this.orderId = orderId;
            this.parentOrderId = parentOrderId;
            this.orderType = orderType;
        }
        public WebsocketThrottleOrderRequest(string orderId, string parentOrderId,
            PlatformEnum platform, CalculateThrottle throttleCalculator, OrderTypeEnum orderType)
        {
            this.orderId = orderId;
            this.parentOrderId = parentOrderId;
            this.throttleCalculator = throttleCalculator;
            this.orderType = orderType;
        }
    }
}