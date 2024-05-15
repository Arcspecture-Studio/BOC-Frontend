using System;

namespace Binance
{
    [Serializable]
    public class WebrequestRequest : Request
    {
        public WebrequestRequest(string platformId) : base(platformId)
        {
        }
    }
}