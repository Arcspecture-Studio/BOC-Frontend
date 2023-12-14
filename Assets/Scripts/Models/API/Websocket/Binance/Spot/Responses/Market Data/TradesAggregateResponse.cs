using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class TradesAggregateResponse : ResponseWrapper<List<TradesAggregateResult>>
    {
    }

    [Serializable]
    public class TradesAggregateResult
    {
        public long a;      // Aggregate trade ID
        public string p;    // Price
        public string q;    // Quantity
        public long f;      // First trade ID
        public long I;      // Last trade ID
        public long T;      // Timestamp
        public bool m;      // Was the buyer the maker?
        public bool M;      // Was the trade the best price match?
    }
}