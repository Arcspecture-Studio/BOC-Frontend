using System;

namespace General
{
    [Serializable]
    public class WebsocketCallApiRequest : WebsocketGeneralRequest
    {
        public Request request;

        public WebsocketCallApiRequest(string token, Request request) : base(WebsocketEventTypeEnum.CALL_API, token)
        {
            this.request = request;
        }
    }
}