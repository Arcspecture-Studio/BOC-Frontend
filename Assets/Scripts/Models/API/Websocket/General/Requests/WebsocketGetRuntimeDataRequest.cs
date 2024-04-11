using System;

namespace General
{
    [Serializable]
    public class WebsocketGetRuntimeDataRequest : WebsocketGeneralRequest
    {
        public PlatformEnum platform;

        public WebsocketGetRuntimeDataRequest(string token, PlatformEnum platform) : base(WebsocketEventTypeEnum.GET_RUNTIME_DATA, token)
        {
            this.platform = platform;
        }
    }
}