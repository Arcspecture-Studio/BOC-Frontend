using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class ResponseWrapper<T> : GeneralResponse
    {
        public T result;
        public List<RateLimitWithCount> rateLimits;
    }

    [Serializable]
    public class RateLimitWithCount : RateLimit
    {
        public long count;
    }

    [Serializable]
    public class RateLimit
    {
        public string rateLimitType;
        public string interval;
        public long intervalNum;
        public long limit;
    }
}