using System;

namespace General
{
    [Serializable]
    public class WebsocketAddQuickOrderResponse : WebsocketGeneralResponse
    {
        public WebsocketGetQuickOrderResponse quickOrder;
    }
}