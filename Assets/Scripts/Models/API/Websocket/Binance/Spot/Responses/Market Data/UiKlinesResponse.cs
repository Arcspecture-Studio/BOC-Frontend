using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class UiKlinesResponse : ResponseWrapper<List<List<object>>>
    {
    }

    // List<List<object>> result: [
    //    [
    //      1655971200000,      // Kline open time
    //      "0.01086000",       // Open price
    //      "0.01086600",       // High price
    //      "0.01083600",       // Low price
    //      "0.01083800",       // Close price
    //      "2290.53800000",    // Volume
    //      1655974799999,      // Kline close time
    //      "24.85074442",      // Quote asset volume
    //      2283,               // Number of trades
    //      "1171.64000000",    // Taker buy base asset volume
    //      "12.71225884",      // Taker buy quote asset volume
    //      "0"                 // Unused field, ignore
    //    ]
    // ]
}