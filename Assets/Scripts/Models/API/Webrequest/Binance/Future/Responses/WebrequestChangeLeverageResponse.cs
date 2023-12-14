using System;

namespace Binance
{
    [Serializable]
    public class WebrequestChangeLeverageResponse : WebrequestGeneralResponse
    {
        public long leverage;
        public string maxNotionalValue;
        public string symbol;
    }
}