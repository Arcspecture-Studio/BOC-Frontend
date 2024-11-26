#pragma warning disable CS8632

using System;

namespace General
{
    [Serializable]
    public class WebsocketSpawnOrderResponse : WebsocketGeneralResponse
    {
        public WebsocketGetOrderDataResponse order;
    }
}