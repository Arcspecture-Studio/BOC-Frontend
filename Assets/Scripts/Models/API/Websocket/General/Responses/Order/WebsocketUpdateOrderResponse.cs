using System;

namespace General
{
    [Serializable]
    public class WebsocketUpdateOrderResponse : WebsocketGeneralResponse
    {
        public WebsocketUpdateOrderDataResponse order;
    }
    [Serializable]
    public class WebsocketUpdateOrderDataResponse
    {
        public string id;
        public MarginCalculator marginCalculator;
    }
}