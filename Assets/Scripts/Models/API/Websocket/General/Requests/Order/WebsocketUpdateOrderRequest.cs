using System;

namespace General
{
    [Serializable]
    public class WebsocketUpdateOrderRequest : WebsocketOrderRequest
    {
        public bool? disableExit;

        public WebsocketUpdateOrderRequest(string token,
            string orderId,
            MarginCalculatorUpdate marginCalculator,
            OrderTypeEnum orderType,
            FundingFeeHandlerEnum fundingFeeHandler,
            bool disableExit,
            string tradingBotId) : base(WebsocketEventTypeEnum.UPDATE_ORDER, token, orderId,
            null, marginCalculator, orderType, fundingFeeHandler, tradingBotId)
        {
            this.disableExit = disableExit;
        }
    }
}