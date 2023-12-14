using System;

namespace Binance
{
    [Serializable]
    public class WebrequestGetAccInfoRequest : WebrequestRequest
    {
        public WebrequestGetAccInfoRequest(bool testnet, string apiSecret) : base(testnet)
        {
            path = "/fapi/v2/account";
            requestType = WebrequestRequestTypeEnum.GET;

            UpdateUri(null, apiSecret);
        }
    }
}