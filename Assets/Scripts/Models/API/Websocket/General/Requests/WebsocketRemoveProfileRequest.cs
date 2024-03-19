using System;

namespace General
{
    [Serializable]
    public class WebsocketRemoveProfileRequest : WebsocketGeneralRequest
    {
        public string profileId;

        public WebsocketRemoveProfileRequest(string token, string profileId) : base(WebsocketEventTypeEnum.REMOVE_PROFILE, token)
        {
            this.profileId = profileId;
        }
    }
}