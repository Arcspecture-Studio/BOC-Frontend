using System;

namespace General
{
    [Serializable]
    public class WebsocketCallApiRequest : WebsocketGeneralRequest
    {
        public Request request;

        public WebsocketCallApiRequest(Request request) : base(WebsocketEventTypeEnum.CALL_API)
        {
            this.request = request;
        }
    }
}