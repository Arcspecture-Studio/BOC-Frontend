using System;

namespace General
{
    [Serializable]
    public class WebsocketIdRequest
    {
        public string _id;
        public WebsocketIdRequest(string orderId)
        {
            _id = orderId;
        }
    }
}