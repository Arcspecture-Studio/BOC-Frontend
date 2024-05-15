using System;

namespace General
{
    [Serializable]
    public class WebsocketRemovePlatformRequest : WebsocketIdRequest
    {
        public WebsocketRemovePlatformRequest(string token, string platformId) : base(WebsocketEventTypeEnum.REMOVE_PLATFORM, token, platformId)
        {
        }
    }
}