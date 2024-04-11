using System;

namespace General
{
    [Serializable]
    public class WebsocketCallApiResponse : WebsocketGeneralResponse
    {
        public string id;
        public string responseJsonString;
        public bool? rejectByServer;
    }
}