using System;

namespace General
{
    [Serializable]
    public class WebsocketAddQuickOrderRequest : WebsocketQuickOrderRequest
    {
        public WebsocketAddQuickOrderRequest(string token,
            float entryPrice,
            bool isLong) : base(WebsocketEventTypeEnum.ADD_QUICK_ORDER, token, entryPrice, isLong)
        {
        }
    }
}