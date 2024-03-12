#pragma warning disable CS8632

using System;

namespace Binance
{
    [Serializable]
    public class WebrequestGetMultiAssetsModeResponse : WebrequestGeneralResponse
    {
        public bool? multiAssetsMargin;
    }
}