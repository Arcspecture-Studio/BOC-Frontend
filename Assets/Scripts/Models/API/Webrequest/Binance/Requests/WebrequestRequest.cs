using System;

namespace Binance
{
    [Serializable]
    public class WebrequestRequest : Request
    {
        public WebrequestRequest(bool testnet)
        {
            platform = testnet ? PlatformEnum.BINANCE_TESTNET : PlatformEnum.BINANCE;
        }
    }
}