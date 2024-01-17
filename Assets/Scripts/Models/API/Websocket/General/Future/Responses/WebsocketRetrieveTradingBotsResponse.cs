using System;
using System.Collections.Generic;

namespace General
{
    [Serializable]
    public class WebsocketRetrieveTradingBotsResponse : WebsocketGeneralResponse
    {
        public Dictionary<string, WebsocketRetrieveTradingBotsResponseData> tradingBots;
    }
    [Serializable]
    public class WebsocketRetrieveTradingBotsResponseData
    {
        public BotTypeEnum botType;
        public QuickOrderSetting quickOrderSetting;
    }
}