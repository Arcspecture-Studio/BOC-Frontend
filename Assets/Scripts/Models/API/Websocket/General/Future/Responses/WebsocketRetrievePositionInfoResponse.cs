#pragma warning disable CS8632

using System;

namespace General
{
    [Serializable]
    public class WebsocketRetrievePositionInfoResponse : WebsocketGeneralResponse
    {
        public string orderId;
        public double? quantityFilled;
        public double? averagePriceFilled;
        public double? actualTakeProfitPrice;
        public double? paidFundingAmount;
    }
}