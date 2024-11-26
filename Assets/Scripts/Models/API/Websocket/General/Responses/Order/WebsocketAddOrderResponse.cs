using System;

namespace General
{
    [Serializable]
    public class WebsocketAddOrderResponse : WebsocketGeneralResponse
    {
        public WebsocketAddOrderDataResponse order;
    }
    [Serializable]
    public class WebsocketAddOrderDataResponse : WebsocketUpdateOrderDataResponse
    {
        public long spawnTime; // TIMESTAMP
        public ExitOrderTypeEnum exitOrderType;
    }
}