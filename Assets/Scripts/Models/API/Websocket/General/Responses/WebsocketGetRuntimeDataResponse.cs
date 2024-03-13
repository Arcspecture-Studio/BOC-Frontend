#pragma warning disable CS8632

using System;

namespace General
{
    [Serializable]
    public class WebsocketGetRuntimeDataResponse : WebsocketGeneralResponse
    {
        public string? orders;
        public string? quickOrders;
        public string? tradingBots;
    }
}