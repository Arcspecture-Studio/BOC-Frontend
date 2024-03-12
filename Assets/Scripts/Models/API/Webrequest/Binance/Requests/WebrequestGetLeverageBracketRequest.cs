using System;

namespace Binance
{
    [Serializable]
    public class WebrequestGetLeverageBracketRequest : WebrequestRequest
    {
        public WebrequestGetLeverageBracketRequest(bool testnet, string apiSecret, string symbol) : base(testnet)
        {
            path = "/fapi/v1/leverageBracket";
            requestType = WebrequestRequestTypeEnum.GET;

            string queries = "symbol=" + symbol;
            UpdateUri(queries, apiSecret);
        }
    }
}