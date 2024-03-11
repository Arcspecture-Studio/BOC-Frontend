using System;

namespace General
{
    [Serializable]
    public class WebsocketGeneralResponse
    {
        public WebsocketEventTypeEnum eventType;
        public long eventTime;
    }
}