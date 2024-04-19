using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class GetRuntimeDataSystem : MonoBehaviour
{
    GetRuntimeDataComponent getRuntimeDataComponent;
    SpawnOrderComponent spawnOrderComponent;
    SpawnQuickOrderComponent spawnQuickOrderComponent;
    SpawnBotComponent spawnTradingBotComponent;
    WebsocketComponent websocketComponent;
    LoginComponent loginComponent;
    PlatformComponent platformComponent;
    PromptComponent promptComponent;
    OrderPagesComponent orderPagesComponent;
    QuickTabComponent quickTabComponent;
    BotTabComponent botTabComponent;
    GetBalanceComponent getBalanceComponent;

    void Start()
    {
        getRuntimeDataComponent = GlobalComponent.instance.getRuntimeDataComponent;
        spawnOrderComponent = GlobalComponent.instance.spawnOrderComponent;
        spawnQuickOrderComponent = GlobalComponent.instance.spawnQuickOrderComponent;
        spawnTradingBotComponent = GlobalComponent.instance.spawnTradingBotComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        promptComponent = GlobalComponent.instance.promptComponent;
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;
        quickTabComponent = GlobalComponent.instance.quickTabComponent;
        botTabComponent = GlobalComponent.instance.botTabComponent;
        getBalanceComponent = GlobalComponent.instance.getBalanceComponent;

        getRuntimeDataComponent.onChange_getRuntimeData.AddListener(GetRuntimeData);
        getRuntimeDataComponent.onChange_processRuntimeData.AddListener(ProcessRuntimeData);
    }
    void Update()
    {
        GetRuntimeDataResponse();
    }

    void GetRuntimeData()
    {
        General.WebsocketGetRuntimeDataRequest request = new(loginComponent.token, platformComponent.activePlatform);
        websocketComponent.generalRequests.Add(request);
    }
    void GetRuntimeDataResponse()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.GET_RUNTIME_DATA);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.GET_RUNTIME_DATA);
        if (jsonString.IsNullOrEmpty()) return;

        General.WebsocketGetRuntimeDataResponse response = JsonConvert.DeserializeObject
        <General.WebsocketGetRuntimeDataResponse>(jsonString, JsonSerializerConfig.settings);

        if (!response.success)
        {
            promptComponent.ShowPrompt(PromptConstant.ERROR, response.message, () =>
            {
                promptComponent.active = false;
            });
            return;
        }

        ProcessRuntimeData(response);
    }
    void ProcessRuntimeData(General.WebsocketGetRuntimeDataResponse runtimeData)
    {
        getBalanceComponent.processBalance = runtimeData.balances;
        DestroyOrders();
        DestroyQuickOrders();
        DestroyTradingBots();
        foreach (General.WebsocketGetOrderResponse order in runtimeData.orders)
        {
            spawnOrderComponent.orderToSpawn = order;
        }
        foreach (General.WebsocketGetQuickOrderResponse quickOrder in runtimeData.quickOrders)
        {
            spawnQuickOrderComponent.quickOrderToSpawn = quickOrder;
        }
        foreach (General.WebsocketGetTradingBotResponse tradingBot in runtimeData.tradingBots)
        {
            spawnTradingBotComponent.botToSpawn = tradingBot;
        }

        if (platformComponent.gameObject.activeSelf)
        {
            platformComponent.backButton.interactable = true;
            platformComponent.gameObject.SetActive(false);
        }
    }
    void DestroyOrders()
    {
        if (orderPagesComponent.transform.childCount == 0) return;
        for (int i = 0; i < orderPagesComponent.transform.childCount; i++)
        {
            Destroy(orderPagesComponent.transform.GetChild(i).gameObject);
        }
        orderPagesComponent.childOrderPageComponents = new();
        orderPagesComponent.currentPageIndex = 0;
        // orderPagesComponent.scaleOrders = true;
    }
    void DestroyQuickOrders()
    {
        if (quickTabComponent.spawnedQuickOrderDataObjects.Count == 0) return;
        foreach (KeyValuePair<string, GameObject> obj in quickTabComponent.spawnedQuickOrderDataObjects)
        {
            Destroy(obj.Value);
        }
        quickTabComponent.spawnedQuickOrderDataObjects.Clear();
    }
    void DestroyTradingBots()
    {
        if (botTabComponent.spawnedBotDataObjects.Count == 0) return;
        foreach (KeyValuePair<string, GameObject> obj in botTabComponent.spawnedBotDataObjects)
        {
            Destroy(obj.Value);
        }
        botTabComponent.spawnedBotDataObjects.Clear();
    }
}