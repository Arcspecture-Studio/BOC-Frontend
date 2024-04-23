using System;

namespace General
{
    [Serializable]
    public class WebsocketAddTradingBotRequest : WebsocketGeneralRequest
    {
        public WebsocketTradingBotRequest botRequest;

        public WebsocketAddTradingBotRequest(string token,
            TradingBotSetting botSetting) :
        base(WebsocketEventTypeEnum.ADD_TRADING_BOT, token)
        {
            botRequest = new(botSetting);
        }
    }
}