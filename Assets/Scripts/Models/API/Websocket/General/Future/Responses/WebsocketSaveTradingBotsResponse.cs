using System;

namespace General
{
    [Serializable]
    public class WebsocketSaveTradingBotsResponse : WebsocketGeneralResponse
    {
        public bool tradingBotSpawned;
    }
}