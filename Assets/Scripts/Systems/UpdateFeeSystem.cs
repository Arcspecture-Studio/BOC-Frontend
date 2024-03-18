#pragma warning disable CS8632

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class UpdateFeeSystem : MonoBehaviour
{
    OrderPageSymbolDropdownComponent orderPageSymbolDropdownComponent;
    PlatformComponentOld platformComponentOld;
    WebrequestComponent webrequestComponent;

    List<Request> requests;
    string selectedSymbol;
    bool resend = false;

    void Start()
    {
        orderPageSymbolDropdownComponent = GetComponent<OrderPageSymbolDropdownComponent>();
        platformComponentOld = GlobalComponent.instance.platformComponentOld;
        webrequestComponent = GlobalComponent.instance.webrequestComponent;

        requests = new List<Request>();
    }
    void Update()
    {
        UpdateFees();
        ReceiveFees();
        ResendGetFee();
    }

    void UpdateFees()
    {
        if (selectedSymbol == orderPageSymbolDropdownComponent.selectedSymbol) return;
        selectedSymbol = orderPageSymbolDropdownComponent.selectedSymbol;
        if (selectedSymbol.Length <= 0) return;
        if (!platformComponentOld.fees.ContainsKey(selectedSymbol))
        {
            platformComponentOld.fees.Add(selectedSymbol, null);
            GetFee();
        }
    }
    void ResendGetFee()
    {
        if (!resend) return;
        resend = false;
        GetFee();
    }
    void GetFee()
    {
        // Request request = General.WebrequestFeeRequest.Get(platformComponentOld.activePlatform, platformComponentOld.apiSecret, selectedSymbol);
        // requests.Add(request);
        // webrequestComponent.requests.Add(request);
    }
    void ReceiveFees()
    {
        if (requests.Count == 0) return;
        for (int i = requests.Count - 1; i >= 0; i--)
        {
            if (webrequestComponent.responses.ContainsKey(requests[i].id))
            {
                JObject response = JsonConvert.DeserializeObject<JObject>(webrequestComponent.responses[requests[i].id], JsonSerializerConfig.settings);
                switch (requests[i].platform)
                {
                    case PlatformEnum.BINANCE:
                    case PlatformEnum.BINANCE_TESTNET:
                        string? symbol = response.ContainsKey("symbol") ? (string)response["symbol"] : null;
                        string? makerCommissionRate = response.ContainsKey("makerCommissionRate") ? (string)response["makerCommissionRate"] : null;
                        string? takerCommissionRate = response.ContainsKey("takerCommissionRate") ? (string)response["takerCommissionRate"] : null;
                        if (symbol.IsNullOrEmpty())
                        {
                            resend = true;
                        }
                        else
                        {
                            platformComponentOld.fees[symbol] = Math.Max(double.Parse(makerCommissionRate), double.Parse(takerCommissionRate));
                        }
                        break;
                }
                webrequestComponent.responses.Remove(requests[i].id);
                requests.RemoveAt(i);
            }
        }
    }
}
