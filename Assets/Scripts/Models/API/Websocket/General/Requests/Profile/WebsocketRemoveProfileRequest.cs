using System;

namespace General
{
    [Serializable]
    public class WebsocketRemoveProfileRequest : WebsocketIdRequest
    {
        public WebsocketRemoveProfileRequest(string token, string profileId) : base(WebsocketEventTypeEnum.REMOVE_PROFILE, token, profileId)
        {
        }
    }
}