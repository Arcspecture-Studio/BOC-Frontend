using System;

namespace General
{
    [Serializable]
    public class WebsocketAddOrderResponse : WebsocketGeneralResponse
    {
        public string id;
        public long spawnTime; // TIMESTAMP
    }
}