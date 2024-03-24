using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class GetRuntimeDataSystem : MonoBehaviour
{
    GetRuntimeDataComponent getRuntimeDataComponent;
    SpawnOrderComponent spawnOrderComponent;
    SpawnQuickOrderComponent spawnQuickOrderComponent;
    SpawnTradingBotComponent spawnTradingBotComponent;
    WebsocketComponent websocketComponent;
    LoginComponent loginComponent;
    PlatformComponent platformComponent;
    PromptComponent promptComponent;
    OrderPagesComponent orderPagesComponent;

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
        if (jsonString.IsNullOrEmpty()) return;
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.GET_RUNTIME_DATA);

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

        DestroyExistingOrdersObject();
        ProcessRuntimeData(response);
    }
    void ProcessRuntimeData(General.WebsocketGetRuntimeDataResponse runtimeData)
    {
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
    }
    void DestroyExistingOrdersObject()
    {
        if (orderPagesComponent.transform.childCount == 0) return;
        for (int i = 0; i < orderPagesComponent.transform.childCount; i++)
        {
            Destroy(orderPagesComponent.transform.GetChild(i).gameObject);
        }
        orderPagesComponent.childRectTransforms.Clear();
        orderPagesComponent.childOrderPageComponents.Clear();
        orderPagesComponent.currentPageIndex = 0;
        orderPagesComponent.scaleOrders = true;
    }
}