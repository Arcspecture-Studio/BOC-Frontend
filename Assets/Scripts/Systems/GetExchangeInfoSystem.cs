using System.Collections.Generic;
using UnityEngine;

public class GetExchangeInfoSystem : MonoBehaviour
{
    GetExchangeInfoComponent getExchangeInfoComponent;
    PlatformComponent platformComponent;

    void Start()
    {
        getExchangeInfoComponent = GlobalComponent.instance.getExchangeInfoComponent;
        platformComponent = GlobalComponent.instance.platformComponent;

        getExchangeInfoComponent.onChange_processExchangeInfo.AddListener(ProcessExchangeInfo);
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