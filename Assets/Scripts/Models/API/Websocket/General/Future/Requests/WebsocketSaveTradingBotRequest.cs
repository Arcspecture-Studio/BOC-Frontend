using System;

namespace General
{
    [Serializable]
    public class WebsocketSaveTradingBotRequest : WebsocketGeneralRequest
    {
        public WebsocketDataActionEnum actionToTake;

        public WebsocketSaveTradingBotRequest(PlatformEnum platform) : base(WebsocketEventTypeEnum.SAVE_TRADING_BOT, platform)
        {
        }
    }
}