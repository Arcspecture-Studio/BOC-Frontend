using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class TradesHistoricalResponse : ResponseWrapper<List<TradesHistoricalResult>>
    {
    }

    [Serializable]
    public class TradesHistoricalResult : TradesRecentResult
    {
    }
}