using System;

namespace General
{
    [Serializable]
    public class WebsocketDeleteTradingBotRequest : WebsocketGeneralRequest
    {
        public WebsocketIdRequest botRequest;

        public WebsocketDeleteTradingBotRequest(string token,
            string botId) : base(WebsocketEventTypeEnum.DELETE_TRADING_BOT, token)
        {
            botRequest = new(botId);
        }
    }
}