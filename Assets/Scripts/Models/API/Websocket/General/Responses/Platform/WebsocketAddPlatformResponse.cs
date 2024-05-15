using System;

namespace General
{
    [Serializable]
    public class WebsocketAddPlatformResponse : WebsocketGeneralResponse
    {
        public string platformId;
        public PlatformEnum platform;
    }
}