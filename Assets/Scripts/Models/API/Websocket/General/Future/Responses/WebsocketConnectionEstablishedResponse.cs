using System;

namespace General
{
    [Serializable]
    public class WebsocketConnectionEstablishedResponse : WebsocketGeneralResponse
    {
        public WebsocketConnectionEstablishedResponseIv iv;
    }

    [Serializable]
    public class WebsocketConnectionEstablishedResponseIv
    {
        public string type;
        public byte[] data;
    }
}
