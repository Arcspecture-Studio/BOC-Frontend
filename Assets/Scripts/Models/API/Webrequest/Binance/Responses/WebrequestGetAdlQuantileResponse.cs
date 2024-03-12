using System;

namespace Binance
{
    [Serializable]
    public class WebrequestGetAdlQuantileResponse : WebrequestGeneralResponse
    {
        public string symbol;
        public WebrequestGetAdlQuantileResponseData adlQuantile;
    }

    [Serializable]
    public class WebrequestGetAdlQuantileResponseData
    {
        public long LONG;
        public long SHORT;
        public long HEDGE;
        public long BOTH;
    }
}