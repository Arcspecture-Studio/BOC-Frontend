using System;

namespace General
{
    [Serializable]
    public class WebsocketIdRequest : WebsocketGeneralRequest
    {
        public string _id;
        public WebsocketIdRequest(WebsocketEventTypeEnum eventType, string token, string orderId) :
        base(eventType, token)
        {
            _id = orderId;
        }
    }
}