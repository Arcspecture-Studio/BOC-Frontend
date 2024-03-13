using System;

namespace General
{
    [Serializable]
    public class WebsocketRemovePlatformRequest : WebsocketGeneralRequest
    {
        public PlatformEnum platform;

        public WebsocketRemovePlatformRequest(string token, PlatformEnum platform) : base(WebsocketEventTypeEnum.REMOVE_PLATFORM, token)
        {
            this.platform = platform;
        }
    }
}