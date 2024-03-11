using System;

namespace General
{
    [Serializable]
    public class WebsocketSyncApiKeyRequest : WebsocketGeneralRequest
    {
        public string apiKey;
        public string apiSecret;
        public string loginPhrase;

        public WebsocketSyncApiKeyRequest(string apiKey, string apiSecret, PlatformEnum platform, string loginPhrase, string version) : base(WebsocketEventTypeEnum.SYNC_API_KEY)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            this.loginPhrase = loginPhrase;
        }
    }
}