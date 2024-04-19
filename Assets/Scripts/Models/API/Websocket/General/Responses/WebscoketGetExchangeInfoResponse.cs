using System;
using System.Collections.Generic;

namespace General
{
    [Serializable]
    public class WebsocketGetExchangeInfoResponse : WebsocketGeneralResponse
    {
        public Dictionary<string, WebsocketGetExchangeInfo> exchangeInfos;
    }
}