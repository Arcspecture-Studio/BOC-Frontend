#pragma warning disable CS8632

using System;
using WebSocketSharp;

namespace Binance
{
    [Serializable]
    public class WebrequestGetAdlQuantileRequest : WebrequestRequest
    {
        public WebrequestGetAdlQuantileRequest(bool testnet, string apiSecret, string? symbol) : base(testnet)
        {
            path = "/fapi/v1/adlQuantile";
            requestType = WebrequestRequestTypeEnum.GET;

            if (symbol.IsNullOrEmpty())
            {
                UpdateUri(null, apiSecret);
            }
            else
            {
                string queries = "symbol=" + symbol;
                UpdateUri(queries, apiSecret);
            }
        }
    }
}