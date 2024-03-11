#pragma warning disable CS8632

using System;

namespace General
{
    [Serializable]
    public class WebsocketSpawnQuickOrderResponse : WebsocketGeneralResponse
    {
        public string orderId;
        public WebsocketRetrieveQuickOrdersData? quickOrder;
    }
}