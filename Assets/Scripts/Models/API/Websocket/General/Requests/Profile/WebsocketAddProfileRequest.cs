using System;

namespace General
{
    [Serializable]
    public class WebsocketAddProfileRequest : WebsocketGeneralRequest
    {
        public string name;
        public PlatformEnum activePlatform;

        public WebsocketAddProfileRequest(string token, string name, PlatformEnum activePlatform) : base(WebsocketEventTypeEnum.ADD_PROFILE, token)
        {
            this.name = name;
            this.activePlatform = activePlatform;
        }
    }
}