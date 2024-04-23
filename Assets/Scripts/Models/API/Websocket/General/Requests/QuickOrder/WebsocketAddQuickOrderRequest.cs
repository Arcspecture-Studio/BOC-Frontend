using System;

namespace General
{
    [Serializable]
    public class WebsocketAddQuickOrderRequest : WebsocketGeneralRequest
    {
        public WebsocketQuickOrderRequest orderRequest;

        public WebsocketAddQuickOrderRequest(string token,
            double entryPrice,
            bool isLong) : base(WebsocketEventTypeEnum.ADD_QUICK_ORDER, token)
        {
            orderRequest = new(entryPrice, isLong);
        }
    }
}