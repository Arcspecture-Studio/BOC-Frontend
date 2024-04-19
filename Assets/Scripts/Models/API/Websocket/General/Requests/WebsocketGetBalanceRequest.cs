using System;

namespace General
{
    [Serializable]
    public class WebsocketGetBalanceRequest : WebsocketGeneralRequest
    {
        public PlatformEnum platform;

        public WebsocketGetBalanceRequest(string token, PlatformEnum platform) : base(WebsocketEventTypeEnum.GET_BALANCE, token)
        {
            this.platform = platform;
        }
    }
}