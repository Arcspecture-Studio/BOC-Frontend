#pragma warning disable CS8632

using System;

namespace General
{
    [Serializable]
    public class WebsocketPositionInfoUpdateResponse : WebsocketGeneralResponse
    {
        public string orderId;
        public float? quantityFilled;
        public float? averagePriceFilled;
        public float? actualTakeProfitPrice;
        public float? actualStopLossPrice;
        public float? actualBreakEvenPrice;
        public float? paidFundingAmount;
        public bool? removeBot;
        public ExitOrderTypeEnum? exitOrderType;
    }
}