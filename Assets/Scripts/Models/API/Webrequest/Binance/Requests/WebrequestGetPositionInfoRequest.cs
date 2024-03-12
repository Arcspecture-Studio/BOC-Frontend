#pragma warning disable CS8632

using System;
using WebSocketSharp;

namespace Binance
{
    [Serializable]
    public class WebrequestGetPositionInfoRequest : WebrequestRequest
    {
        public WebrequestGetPositionInfoRequest(bool testnet, string apiSecret, string? symbol) : base(testnet)
        {
            path = "/fapi/v1/positionRisk";
            requestType = WebrequestRequestTypeEnum.GET;

            string symbolQuery = symbol.IsNullOrEmpty() ? "" : ("symbol=" + symbol);
            string queries = symbolQuery;
            UpdateUri(queries, apiSecret);
        }
    }
}