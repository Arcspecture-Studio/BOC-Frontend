using System;

namespace General
{
    [Serializable]
    public class WebsocketGeneralResponse
    {
        public WebsocketEventTypeEnum eventType;
        public long eventTime; // use long because the timestamp tend to be long
        public bool success;
        public string message;
    }
}