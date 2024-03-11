using System;
using UnityEngine;

namespace General
{
    [Serializable]
    public class WebsocketGeneralRequest
    {
        public WebsocketEventTypeEnum eventType;
        public long eventTime;
        public string version;

        public WebsocketGeneralRequest(WebsocketEventTypeEnum eventType)
        {
            this.eventType = eventType;
            this.eventTime = Utils.CurrentTimestamp();
            this.version = Application.version;
        }
    }
}