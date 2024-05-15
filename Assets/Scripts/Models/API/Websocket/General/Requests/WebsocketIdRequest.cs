using System;

namespace General
{
    [Serializable]
    public class WebsocketIdRequest : WebsocketGeneralRequest
    {
        public string id;
        public WebsocketIdRequest(WebsocketEventTypeEnum eventType, string token, string id) :
        base(eventType, token)
        {
            this.id = id;
        }
    }
}