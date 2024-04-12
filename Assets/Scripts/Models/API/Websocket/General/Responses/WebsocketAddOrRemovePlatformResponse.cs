using System;

namespace General
{
    [Serializable]
    public class WebsocketAddOrRemovePlatformResponse : WebsocketGeneralResponse
    {
        public PlatformEnum platform;
        public PlatformEnum newActivePlatform;
    }
}