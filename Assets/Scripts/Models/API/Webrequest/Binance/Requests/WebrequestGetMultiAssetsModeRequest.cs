using System;

namespace Binance
{
    [Serializable]
    public class WebrequestGetMultiAssetsModeRequest : WebrequestRequest
    {
        public WebrequestGetMultiAssetsModeRequest(bool testnet, string apiSecret) : base(testnet)
        {
            path = "/fapi/v1/multiAssetsMargin";
            requestType = WebrequestRequestTypeEnum.GET;

            UpdateUri(null, apiSecret);
        }
    }
}