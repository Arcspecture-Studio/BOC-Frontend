using System;

namespace General
{
    [Serializable]
    public class WebsocketLogoutRequest : WebsocketGeneralRequest
    {
        public WebsocketLogoutRequest(PlatformEnum platform) : base(WebsocketEventTypeEnum.LOGOUT)
        {
        }
    }
}