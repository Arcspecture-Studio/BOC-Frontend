using System;

namespace General
{
    [Serializable]
    public class WebsocketAddTradingBotRequest<T> : WebsocketGeneralRequest
    {
        public WebsocketTradingBotRequest<T> botRequest;

        public WebsocketAddTradingBotRequest(string token,
            PlatformEnum platform,
            TradingBotSetting<T> botSetting,
            QuickOrderSetting quickOrderSetting) :
        base(WebsocketEventTypeEnum.ADD_TRADING_BOT, token)
        {
            botRequest = new(platform, botSetting, quickOrderSetting);
        }
    }
}