using System;
using UnityEngine;

namespace General
{
    [Serializable]
    public class WebsocketGeneralRequest
    {
        public WebsocketEventTypeEnum eventType;
        public long eventTime; // TIMESTAMP
        public string version;
        public string token;

        public WebsocketGeneralRequest(WebsocketEventTypeEnum eventType, string token = null)
        {
            this.eventType = eventType;
            this.eventTime = Utils.CurrentTimestamp();
            this.version = Application.version;
            this.token = token;
        }
    }
}