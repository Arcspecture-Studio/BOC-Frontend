#pragma warning disable CS8632

using System;
using WebSocketSharp;

namespace Binance
{
    [Serializable]
    public class WebrequestRequest : Request
    {
        public WebrequestRequest(bool testnet)
        {
            this.platform = testnet ? PlatformEnum.BINANCE_TESTNET : PlatformEnum.BINANCE;
            this.host = testnet ? WebrequestConfig.BINANCE_HOST_TEST : WebrequestConfig.BINANCE_HOST;
        }

        protected void UpdateUri(string? queries, string apiSecret)
        {
            this.queries = queries;
            string queriesFront = queries.IsNullOrEmpty() ? "" : (queries + "&");
            string queriesFull = queriesFront + "recvWindow=" + WebrequestConfig.BINANCE_RECV_WINDOW.ToString() + "&timestamp=" + Utils.CurrentTimestamp().ToString();
            string signature = Signature.Binance(queriesFull, apiSecret);
            uri = host + path + "?" + queriesFull + "&signature=" + signature;
        }
    }
}