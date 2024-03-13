using System;

namespace General
{
    [Serializable]
    public class WebrequestFeeRequest
    {
        public static Request Get(PlatformEnum platform, string apiSecret, string symbol)
        {
            Request request = new Request();
            switch (platform)
            {
                case PlatformEnum.BINANCE:
                    request = new Binance.WebrequestFeeRequest(false, apiSecret, symbol);
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    request = new Binance.WebrequestFeeRequest(true, apiSecret, symbol);
                    break;
            }
            return request;
        }
    }
}