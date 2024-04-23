using System;

namespace General
{
    [Serializable]
    public class WebsocketTradingBotRequest
    {
        public TradingBotSetting botSetting;

        public WebsocketTradingBotRequest(TradingBotSetting botSetting)
        {
            this.botSetting = botSetting;
        }
    }
}