using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class GetExchangeInfoSystem : MonoBehaviour
{
    GetExchangeInfoComponent getExchangeInfoComponent;
    PlatformComponent platformComponent;
    WebsocketComponent websocketComponent;
    LoginComponent loginComponent;

    void Start()
    {
        getExchangeInfoComponent = GlobalComponent.instance.getExchangeInfoComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        loginComponent = GlobalComponent.instance.loginComponent;

        getExchangeInfoComponent.onChange_processExchangeInfo.AddListener(ProcessExchangeInfo);
        getExchangeInfoComponent.onChange_getExchangeInfo.AddListener(GetExchangeInfo);
    }
    void Update()
    {
        GetExchangeInfoResponse();
    }

    void GetExchangeInfo()
    {
        General.WebsocketGetExchangeInfoRequest request = new(loginComponent.token, platformComponent.activePlatform);
        websocketComponent.generalRequests.Add(request);
    }
    void GetExchangeInfoResponse()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.GET_EXCHANGE_INFO);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.GET_EXCHANGE_INFO);
        if (jsonString.IsNullOrEmpty()) return;

        General.WebsocketGetExchangeInfoResponse response = JsonConvert.DeserializeObject
        <General.WebsocketGetExchangeInfoResponse>(jsonString, JsonSerializerConfig.settings);

        ProcessExchangeInfo(response.exchangeInfos);
    }
    void ProcessExchangeInfo(Dictionary<string, General.WebsocketGetExchangeInfo> exchangeInfos)
    {
        platformComponent.allSymbols.Clear();
        platformComponent.marginAssets.Clear();
        platformComponent.quantityPrecisions.Clear();
        platformComponent.pricePrecisions.Clear();
        foreach (KeyValuePair<string, General.WebsocketGetExchangeInfo> exchangeInfo in exchangeInfos)
        {
            platformComponent.allSymbols.Add(exchangeInfo.Value.symbol);
            platformComponent.marginAssets.Add(exchangeInfo.Value.symbol, exchangeInfo.Value.marginAsset);
            platformComponent.quantityPrecisions.Add(exchangeInfo.Value.symbol, exchangeInfo.Value.quantityPrecision);
            platformComponent.pricePrecisions.Add(exchangeInfo.Value.symbol, exchangeInfo.Value.pricePrecision);
        }
    }
}