using System;

namespace General
{
    [Serializable]
    public class WebsocketAddTradingBotRequest : WebsocketGeneralRequest
    {
        public WebsocketAddTradingBotRequest(string token) :
        base(WebsocketEventTypeEnum.ADD_TRADING_BOT, token)
        {
        }
    }
}