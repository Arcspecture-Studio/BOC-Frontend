#pragma warning disable CS8632

using System;

namespace Binance
{
    [Serializable]
    public class WebrequestGetPositionModeResponse : WebrequestGeneralResponse
    {
        public bool? dualSidePosition;
    }
}