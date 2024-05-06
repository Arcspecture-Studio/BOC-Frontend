using System;

namespace General
{
    [Serializable]
    public class WebsocketAddOrRemovePlatformResponse : WebsocketGeneralResponse
    {
        // TODO
        public PlatformEnum platform;
        public PlatformEnum newActivePlatform;
    }
}