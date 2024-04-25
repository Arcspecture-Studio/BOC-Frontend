using System;

namespace General
{
    [Serializable]
    public class WebsocketDeleteTradingBotRequest : WebsocketIdRequest
    {
        public WebsocketDeleteTradingBotRequest(string token, string botId) :
        base(WebsocketEventTypeEnum.DELETE_TRADING_BOT, token, botId)
        {
        }
    }
}