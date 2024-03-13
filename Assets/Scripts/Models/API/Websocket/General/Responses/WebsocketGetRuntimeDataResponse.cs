using System;

namespace General
{
    [Serializable]
    public class WebsocketGetRuntimeDataResponse : WebsocketGeneralResponse
    {
        public object orders;
        public object quickOrders;
        public object tradingBots;
    }
}