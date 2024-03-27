#pragma warning disable CS8632

using System;

namespace General
{
    [Serializable]
    public class WebsocketSubmitOrderResponse : WebsocketGeneralResponse
    {
        public string orderId;
        public OrderStatusEnum status;
        public bool statusError;
        public string? errorJsonString;
    }
}