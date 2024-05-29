using System;

namespace General
{
    [Serializable]
    public class WebsocketGetFeeResponse : WebsocketGeneralResponse
    {
        public string symbol;
        public float fee;
    }
}