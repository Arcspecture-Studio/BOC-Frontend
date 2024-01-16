using System;

namespace General
{
    [Serializable]
    public class WebsocketSaveThrottleOrderRequest : WebsocketGeneralRequest
    {
        public WebsocketThrottleOrderIdRequest orderRequest;
        public WebsocketDataActionEnum actionToTake;

        public WebsocketSaveThrottleOrderRequest(string orderId, string parentOrderId, PlatformEnum platform, bool submit = false) : base(WebsocketEventTypeEnum.SAVE_THROTTLE_ORDER, platform)
        {
            if (submit) actionToTake = WebsocketDataActionEnum.SUBMIT;
            else actionToTake = WebsocketDataActionEnum.DELETE;
            orderRequest = new WebsocketThrottleOrderIdRequest(orderId, parentOrderId);
        }
        public WebsocketSaveThrottleOrderRequest(string orderId, string parentOrderId, PlatformEnum platform, OrderTypeEnum orderType) : base(WebsocketEventTypeEnum.SAVE_THROTTLE_ORDER, platform)
        {
            actionToTake = WebsocketDataActionEnum.UPDATE;
            orderRequest = new WebsocketThrottleOrderRequest(orderId, parentOrderId, orderType);
        }
        public WebsocketSaveThrottleOrderRequest(string orderId, string parentOrderId, PlatformEnum platform, CalculateThrottle throttlerCalculator, OrderTypeEnum orderType) : base(WebsocketEventTypeEnum.SAVE_THROTTLE_ORDER, platform)
        {
            actionToTake = WebsocketDataActionEnum.SAVE;
            orderRequest = new WebsocketThrottleOrderRequest(orderId, parentOrderId, throttlerCalculator, orderType);
        }
    }
    [Serializable]
    public class WebsocketThrottleOrderRequest : WebsocketThrottleOrderIdRequest
    {
        public CalculateThrottle throttleCalculator;
        public OrderTypeEnum orderType;

        public WebsocketThrottleOrderRequest(string orderId, string parentOrderId, OrderTypeEnum orderType) : base(orderId, parentOrderId)
        {
            this.orderType = orderType;
        }
        public WebsocketThrottleOrderRequest(string orderId, string parentOrderId, CalculateThrottle throttleCalculator, OrderTypeEnum orderType) : base(orderId, parentOrderId)
        {
            this.throttleCalculator = throttleCalculator;
            this.orderType = orderType;
        }
    }
    [Serializable]
    public class WebsocketThrottleOrderIdRequest : WebsocketOrderIdRequest
    {
        public string parentOrderId;
        public WebsocketThrottleOrderIdRequest(string orderId, string parentOrderId) : base(orderId)
        {
            this.parentOrderId = parentOrderId;
        }
    }
}