using System;

namespace General
{
    [Serializable]
    public class WebsocketRemovePlatformResponse : WebsocketGeneralResponse
    {
        public string platformId;
        public string newActivePlatformId;
    }
}