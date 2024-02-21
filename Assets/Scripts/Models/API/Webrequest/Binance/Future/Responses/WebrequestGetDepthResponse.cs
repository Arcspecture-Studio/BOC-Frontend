using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebrequestGetDepthResponse : WebrequestGeneralResponse
    {
        public List<List<string>> asks;
        public List<List<string>> bids;
    }
}