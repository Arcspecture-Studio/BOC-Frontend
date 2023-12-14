using System;

namespace Binance
{
    [Serializable]
    public class WebrequestChangePositionModeRequest : WebrequestRequest
    {
        public WebrequestChangePositionModeRequest(bool testnet, string apiSecret, bool dualSidePosition) : base(testnet)
        {
            path = "/fapi/v1/positionSide/dual";
            requestType = WebrequestRequestTypeEnum.POST;

            string queries =
                "dualSidePosition=" + dualSidePosition;
            UpdateUri(queries, apiSecret);
        }
    }
}