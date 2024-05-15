using System;

namespace General
{
    [Serializable]
    public class WebsocketAddTradingBotResponse : WebsocketGeneralResponse
    {
        public WebsocketGetTradingBotResponse tradingBot;
    }
}