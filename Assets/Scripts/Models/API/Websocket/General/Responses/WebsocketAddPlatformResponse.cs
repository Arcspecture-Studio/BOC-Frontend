using System;

namespace General
{
    [Serializable]
    public class WebsocketAddPlatformResponse : WebsocketGeneralResponse
    {
        public PlatformEnum platform;
    }
}