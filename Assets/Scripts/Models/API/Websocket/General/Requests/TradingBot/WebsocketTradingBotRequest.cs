using System;

namespace General
{
    [Serializable]
    public class WebsocketTradingBotRequest
    {
        public PlatformEnum platform;
        public TradingBotSetting botSetting;
        public QuickOrderSetting quickOrderSetting;

        public WebsocketTradingBotRequest(PlatformEnum platform, TradingBotSetting botSetting, QuickOrderSetting quickOrderSetting)
        {
            this.platform = platform;
            this.botSetting = botSetting;
            this.quickOrderSetting = quickOrderSetting;
        }
    }
}