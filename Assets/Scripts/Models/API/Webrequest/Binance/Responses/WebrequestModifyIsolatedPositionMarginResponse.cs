using System;

namespace Binance
{
    [Serializable]
    public class WebrequestModifyIsolatedPositionMarginResponse : WebrequestGeneralResponse
    {
        public double amount;
        public long type;
    }
}