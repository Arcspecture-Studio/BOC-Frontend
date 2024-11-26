using System;

namespace General
{
    [Serializable]
    public class WebsocketAddQuickOrderResponse : WebsocketGeneralResponse
    {
        public WebsocketGetQuickOrderDataResponse quickOrder;
    }
}