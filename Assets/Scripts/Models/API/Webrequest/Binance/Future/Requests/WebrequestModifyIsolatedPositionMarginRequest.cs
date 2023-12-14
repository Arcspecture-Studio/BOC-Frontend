using System;

namespace Binance
{
    [Serializable]
    public class WebrequestModifyIsolatedPositionMarginRequest : WebrequestRequest
    {
        public WebrequestModifyIsolatedPositionMarginRequest(bool testnet, string apiSecret, string symbol, string positionSide, double amount, long type) : base(testnet)
        {
            path = "/fapi/v1/positionMargin";
            requestType = WebrequestRequestTypeEnum.POST;

            string queries =
                "symbol=" + symbol +
                "positionSide=" + positionSide +
                "amount=" + amount.ToString() +
                "type=" + type.ToString();
            UpdateUri(queries, apiSecret);
        }
    }
}