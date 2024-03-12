using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Binance
{
    [Serializable]
    public class WebrequestCancelMultipleOrdersRequest : WebrequestRequest
    {
        public WebrequestCancelMultipleOrdersRequest(bool testnet, string apiSecret, string symbol, List<long> orderIds) : base(testnet)
        {
            path = "/fapi/v1/batchOrders";
            requestType = WebrequestRequestTypeEnum.DELETE;

            string queries =
                "symbol=" + symbol +
                "&orderIdList=" + JsonConvert.SerializeObject(orderIds, JsonSerializerConfig.settings);
            UpdateUri(queries, apiSecret);
        }
    }
}