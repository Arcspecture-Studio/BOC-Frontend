using System;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace Binance
{
    [Serializable]
    public class WebrequestPlaceMultipleOrdersRequest : WebrequestRequest
    {
        public WebrequestPlaceMultipleOrdersRequest(bool testnet, string apiSecret, List<WebrequestPlaceOrderRequest> orders) : base(testnet)
        {
            path = "/fapi/v1/batchOrders";
            requestType = WebrequestRequestTypeEnum.POST;

            string queries = "[";
            for (int i = 0; i < orders.Count; i++)
            {
                if (i < orders.Count - 1) queries += (orders[i].jsonString + ",");
                else queries += (orders[i].jsonString + "]");
            }
            queries = "batchOrders=" + UnityWebRequest.EscapeURL(queries);
            UpdateUri(queries, apiSecret);
        }
    }
}