using System;

namespace General
{
    [Serializable]
    public class WebsocketTradingBotRequest // TODO: remove this
    {
        public TradingBotSetting botSetting;

        public WebsocketTradingBotRequest(TradingBotSetting botSetting)
        {
            this.botSetting = botSetting;
        }
    }
}