using System;

namespace General
{
    [Serializable]
    public class WebsocketTradingBotRequest<T>
    {
        public PlatformEnum platform;
        public TradingBotSetting<T> botSetting;
        public QuickOrderSetting quickOrderSetting;

        public WebsocketTradingBotRequest(PlatformEnum platform, TradingBotSetting<T> botSetting, QuickOrderSetting quickOrderSetting)
        {
            this.platform = platform;
            this.botSetting = botSetting;
            this.quickOrderSetting = quickOrderSetting;
        }
    }
}