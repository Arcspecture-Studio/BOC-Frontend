using System;

namespace General
{
    [Serializable]
    public class WebsocketGetExchangeInfoRequest : WebsocketGeneralRequest
    {
        public PlatformEnum platform;

        public WebsocketGetExchangeInfoRequest(string token, PlatformEnum platform) : base(WebsocketEventTypeEnum.GET_EXCHANGE_INFO, token)
        {
            this.platform = platform;
        }
    }
}