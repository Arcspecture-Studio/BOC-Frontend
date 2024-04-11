using System;

namespace General
{
    [Serializable]
    public class WebsocketAddTradingBotRequest : WebsocketGeneralRequest
    {
        public WebsocketTradingBotRequest botRequest;

        public WebsocketAddTradingBotRequest(string token,
            PlatformEnum platform,
            TradingBotSetting botSetting,
            QuickOrderSetting quickOrderSetting) :
        base(WebsocketEventTypeEnum.ADD_TRADING_BOT, token)
        {
            botRequest = new(platform, botSetting, quickOrderSetting);
        }
    }
}