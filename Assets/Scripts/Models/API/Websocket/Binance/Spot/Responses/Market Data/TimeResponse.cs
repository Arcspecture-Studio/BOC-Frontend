using System;

namespace Binance
{
    [Serializable]
    public class TimeResponse : ResponseWrapper<TimeResult>
    {
    }

    [Serializable]
    public class TimeResult
    {
        public long serverTime;
    }
}