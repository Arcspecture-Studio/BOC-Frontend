using System;

namespace Binance
{
    [Serializable]
    public class WebrequestCancelOrderRequest : WebrequestQueryOrderRequest
    {
        public WebrequestCancelOrderRequest(bool testnet, string apiSecret, string symbol, long orderId) : base(testnet, apiSecret, symbol, orderId)
        {
            requestType = WebrequestRequestTypeEnum.DELETE;
        }
    }
}