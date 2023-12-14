using System;

namespace Binance
{
    [Serializable]
    public class WebrequestFeeResponse : WebrequestGeneralResponse
    {
        public string symbol;
        public string makerCommissionRate;
        public string takerCommissionRate;
    }
}