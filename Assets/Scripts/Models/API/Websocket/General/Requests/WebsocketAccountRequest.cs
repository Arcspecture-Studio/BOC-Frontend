using System;

namespace General
{
    [Serializable]
    public class WebsocketAccountRequest : WebsocketGeneralRequest
    {
        public string email;
        public string password;

        public WebsocketAccountRequest(WebsocketEventTypeEnum eventType, string email, string password) : base(eventType)
        {
            this.email = email;
            this.password = password;
        }
    }
}