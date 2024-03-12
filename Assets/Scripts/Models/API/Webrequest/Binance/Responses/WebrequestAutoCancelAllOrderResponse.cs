#pragma warning disable CS8632

using System;

namespace Binance
{
    [Serializable]
    public class WebrequestAutoCancelAllOrderResponse : WebrequestGeneralResponse
    {
        public string? symbol;
        public string? countdownTime;
    }
}