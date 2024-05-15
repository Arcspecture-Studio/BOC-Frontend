using System;

namespace General
{
    [Serializable]
    public class WebsocketGetExchangeInfoRequest : WebsocketGeneralRequest
    {
        public string platformId;

        public WebsocketGetExchangeInfoRequest(string token, string platformId) : base(WebsocketEventTypeEnum.GET_EXCHANGE_INFO, token)
        {
            this.platformId = platformId;
        }
    }
}