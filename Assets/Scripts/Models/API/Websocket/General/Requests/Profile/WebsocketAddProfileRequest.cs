using System;

namespace General
{
    [Serializable]
    public class WebsocketAddProfileRequest : WebsocketGeneralRequest
    {
        public string name;
        public string platformId;

        public WebsocketAddProfileRequest(string token, string name, string platformId) : base(WebsocketEventTypeEnum.ADD_PROFILE, token)
        {
            this.name = name;
            this.platformId = platformId;
        }
    }
}