using System;

namespace Binance
{
    [Serializable]
    public class WebrequestListenKeyRequest : WebrequestRequest
    {
        public WebrequestListenKeyRequest(bool testnet) : base(testnet)
        {
            path = "/fapi/v1/listenKey";
            uri = host + path;
        }
    }

    [Serializable]
    public class WebrequestCreateListenKeyRequest : WebrequestListenKeyRequest
    {
        public WebrequestCreateListenKeyRequest(bool testnet) : base(testnet)
        {
            requestType = WebrequestRequestTypeEnum.POST;
        }
    }

    [Serializable]
    public class WebrequestRenewListenKeyRequest : WebrequestListenKeyRequest
    {
        public WebrequestRenewListenKeyRequest(bool testnet) : base(testnet)
        {
            requestType = WebrequestRequestTypeEnum.PUT;
        }
    }

    [Serializable]
    public class WebrequestDeleteListenKeyRequest : WebrequestListenKeyRequest
    {
        public WebrequestDeleteListenKeyRequest(bool testnet) : base(testnet)
        {
            requestType = WebrequestRequestTypeEnum.DELETE;
        }
    }
}