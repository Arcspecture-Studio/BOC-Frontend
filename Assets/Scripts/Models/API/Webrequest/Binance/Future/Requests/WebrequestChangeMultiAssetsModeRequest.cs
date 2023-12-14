using System;

namespace Binance
{
    [Serializable]
    public class WebrequestChangeMultiAssetsModeRequest : WebrequestRequest
    {
        public WebrequestChangeMultiAssetsModeRequest(bool testnet, string apiSecret, bool multiAssetsMargin) : base(testnet)
        {
            path = "/fapi/v1/multiAssetsMargin";
            requestType = WebrequestRequestTypeEnum.POST;

            string queries =
                "multiAssetsMargin=" + multiAssetsMargin;
            UpdateUri(queries, apiSecret);
        }
    }
}