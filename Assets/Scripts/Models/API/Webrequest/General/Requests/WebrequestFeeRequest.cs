using System;

namespace General
{
    [Serializable]
    public class WebrequestFeeRequest
    {
        public static Request Get(PlatformEnum platform, string symbol)
        {
            Request request = new Request();
            switch (platform)
            {
                case PlatformEnum.BINANCE:
                    request = new Binance.WebrequestFeeRequest(false, symbol);
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    request = new Binance.WebrequestFeeRequest(true, symbol);
                    break;
            }
            return request;
        }
    }
}