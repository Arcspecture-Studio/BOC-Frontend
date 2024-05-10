using System;

namespace General
{
    [Serializable]
    public class WebsocketGetBalanceRequest : WebsocketGeneralRequest
    {
        public string platformId;

        public WebsocketGetBalanceRequest(string token, string platformId) : base(WebsocketEventTypeEnum.GET_BALANCE, token)
        {
            this.platformId = platformId;
        }
    }
}