using System;

namespace General
{
    [Serializable]
    public class WebsocketSaveOrderRequest : WebsocketGeneralRequest
    {
        public WebsocketOrderIdRequest orderRequest;
        public WebsocketDataActionEnum actionToTake;

        public WebsocketSaveOrderRequest(string orderId, PlatformEnum platform, bool submit = false) : base(WebsocketEventTypeEnum.SAVE_ORDER)
        {
            if (submit) actionToTake = WebsocketDataActionEnum.SUBMIT;
            else actionToTake = WebsocketDataActionEnum.DELETE;
            orderRequest = new WebsocketOrderIdRequest(orderId);
        }
        public WebsocketSaveOrderRequest(string orderId,
            PlatformEnum platform,
            CalculateMargin marginCalculator,
            TakeProfitTypeEnum takeProfitType,
            OrderTypeEnum orderType) : base(WebsocketEventTypeEnum.SAVE_ORDER)
        {
            actionToTake = WebsocketDataActionEnum.UPDATE;
            // orderRequest = new WebsocketOrderRequest(orderId, marginCalculator, takeProfitType, orderType);
        }
        public WebsocketSaveOrderRequest(string orderId,
            PlatformEnum platform,
            CalculateMargin marginCalculator,
            string symbol,
            TakeProfitTypeEnum takeProfitType,
            OrderTypeEnum orderType) : base(WebsocketEventTypeEnum.SAVE_ORDER)
        {
            actionToTake = WebsocketDataActionEnum.SAVE;
            // orderRequest = new WebsocketOrderRequest(orderId, marginCalculator, symbol, takeProfitType, orderType);
        }
    }
}