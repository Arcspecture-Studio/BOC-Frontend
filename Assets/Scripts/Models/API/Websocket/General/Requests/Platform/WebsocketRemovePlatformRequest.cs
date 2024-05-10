using System;

namespace General
{
    [Serializable]
    public class WebsocketRemovePlatformRequest : WebsocketGeneralRequest
    {
        public string platformId;

        public WebsocketRemovePlatformRequest(string token, string platformId) : base(WebsocketEventTypeEnum.REMOVE_PLATFORM, token)
        {
            this.platformId = platformId;
        }
    }
}