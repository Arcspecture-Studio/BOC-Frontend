using System;

namespace Binance
{
    [Serializable]
    public class WebrequestGetPositionModeRequest : WebrequestRequest
    {
        public WebrequestGetPositionModeRequest(bool testnet, string apiSecret) : base(testnet)
        {
            path = "/fapi/v1/positionSide/dual";
            requestType = WebrequestRequestTypeEnum.GET;

            UpdateUri(null, apiSecret);
        }
    }
}