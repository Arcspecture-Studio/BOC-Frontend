using System;

namespace General
{
    [Serializable]
    public class WebsocketGeneralResponse
    {
        public WebsocketEventTypeEnum eventType;
        public long eventTime;
        public bool success;
        public string message;
    }
}