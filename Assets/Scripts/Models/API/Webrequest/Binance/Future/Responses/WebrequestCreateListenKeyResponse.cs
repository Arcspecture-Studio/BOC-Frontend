#pragma warning disable CS8632

using System;

namespace Binance
{
    [Serializable]
    public class WebrequestCreateListenKeyResponse : WebrequestGeneralResponse
    {
        public string? listenKey;
    }
}