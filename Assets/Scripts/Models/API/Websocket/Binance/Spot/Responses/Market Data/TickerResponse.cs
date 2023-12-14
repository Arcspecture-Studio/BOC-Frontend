using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class TickerResponse : ResponseWrapper<List<TickerResult>>
    {
    }

    [Serializable]
    public class TickerResult : Ticker24HrResult
    {
    }
}