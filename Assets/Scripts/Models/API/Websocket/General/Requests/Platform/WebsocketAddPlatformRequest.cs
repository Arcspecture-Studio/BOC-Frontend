using System;

namespace General
{
    [Serializable]
    public class WebsocketAddPlatformRequest : WebsocketGeneralRequest
    {
        public string apiKey;
        public string apiSecret;
        public PlatformEnum platform;

        public WebsocketAddPlatformRequest(string token, string apiKey, string apiSecret, PlatformEnum platform) : base(WebsocketEventTypeEnum.ADD_PLATFORM, token)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            this.platform = platform;
        }
    }
}