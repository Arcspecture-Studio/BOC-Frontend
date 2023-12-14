using System;

namespace General
{
    [Serializable]
    public class WebsocketGeneralRequest
    {
        public string eventType;
        public long eventTime;
        public string platform;

        public WebsocketGeneralRequest(WebsocketEventTypeEnum eventType, PlatformEnum platform)
        {
            this.eventType = eventType.ToString();
            this.eventTime = Utils.CurrentTimestamp();
            this.platform = platform.ToString();
        }
    }
}