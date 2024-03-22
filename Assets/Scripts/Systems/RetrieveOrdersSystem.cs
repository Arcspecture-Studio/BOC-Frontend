using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class RetrieveOrdersSystem : MonoBehaviour
{
    WebsocketComponent websocketComponent;
    RetrieveOrdersComponent retrieveOrdersComponent;
    PlatformComponent platformComponent;
    OrderPagesComponent orderPagesComponent;
    QuickTabComponent quickTabComponent;

    bool ordersReceived = false;
    void Start()
    {
        websocketComponent = GlobalComponent.instance.websocketComponent;
        retrieveOrdersComponent = GlobalComponent.instance.retrieveOrdersComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;
        quickTabComponent = GlobalComponent.instance.quickTabComponent;
    }
    void Update()
    {
        GetOrdersFromServer();
        DestroyExistingOrdersObject();
        StartCoroutine(InstantiateOrders());
        // StartCoroutine(UpdateExistingOrdersStatus());
    }

    void GetOrdersFromServer()
    {
        string retrieveOrdersString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.RETRIEVE_ORDERS);
        if (retrieveOrdersString.IsNullOrEmpty()) return;
        General.WebsocketRetrieveOrdersResponse response = JsonConvert.DeserializeObject<General.WebsocketRetrieveOrdersResponse>(retrieveOrdersString, JsonSerializerConfig.settings);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.RETRIEVE_ORDERS);
        if (!retrieveOrdersComponent.ordersFromServer.TryAdd(platformComponent.activePlatform, response.orders))
        {
            retrieveOrdersComponent.ordersFromServer[platformComponent.activePlatform] = response.orders;
        }
        if (!retrieveOrdersComponent.quickOrdersFromServer.TryAdd(platformComponent.activePlatform, response.quickOrders))
        {
            retrieveOrdersComponent.quickOrdersFromServer[platformComponent.activePlatform] = response.quickOrders;
        }
        ordersReceived = true;
    }
    void RequestGetOrdersFromServer()
    {
        General.WebsocketGeneralRequest request = new General.WebsocketGeneralRequest(WebsocketEventTypeEnum.RETRIEVE_ORDERS);
        websocketComponent.generalRequests.Add(request);
        ordersReceived = false;
    }
    void DestroyExistingOrdersObject()
    {
        if (!retrieveOrdersComponent.destroyOrders) return;
        retrieveOrdersComponent.destroyOrders = false;
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
    IEnumerator InstantiateOrders()
    {
        if (!retrieveOrdersComponent.instantiateOrders) yield break;
        retrieveOrdersComponent.instantiateOrders = false;
        orderPagesComponent.status = OrderPagesStatusEnum.DETACH;

        RequestGetOrdersFromServer();

        #region Get orders according to platform
        yield return new WaitUntil(() => ordersReceived);
        Dictionary<string, General.WebsocketRetrieveOrdersData> ordersFromServer = retrieveOrdersComponent.ordersFromServer.ContainsKey(platformComponent.activePlatform) ?
            retrieveOrdersComponent.ordersFromServer[platformComponent.activePlatform] : null;
        quickTabComponent.quickOrdersFromServer = retrieveOrdersComponent.quickOrdersFromServer.ContainsKey(platformComponent.activePlatform) ? retrieveOrdersComponent.quickOrdersFromServer[platformComponent.activePlatform] : null;
        #endregion

        #region Instantiate orders
        if (ordersFromServer != null)
        {
            foreach (KeyValuePair<string, General.WebsocketRetrieveOrdersData> order in ordersFromServer)
            {
                InstantiateOrder(order.Key);
            }
        }
        #endregion
    }
    void InstantiateOrder(string orderId)
    {
        GameObject orderPageObject = Instantiate(orderPagesComponent.orderPagePrefab);
        orderPageObject.transform.SetParent(orderPagesComponent.transform, false);
        OrderPageComponent orderPageComponent = orderPageObject.GetComponent<OrderPageComponent>();
        orderPageComponent.orderId = orderId;
        orderPageComponent.restoreData = true;
    }
    IEnumerator UpdateExistingOrdersStatus() // PENDING: Review this and consider removing it because seems to be useless
    {
        if (!retrieveOrdersComponent.updateOrderStatus) yield break;
        retrieveOrdersComponent.updateOrderStatus = false;

        RequestGetOrdersFromServer();

        #region Get orders according to platform
        yield return new WaitUntil(() => ordersReceived);
        Dictionary<string, General.WebsocketRetrieveOrdersData> ordersFromServer = retrieveOrdersComponent.ordersFromServer.ContainsKey(platformComponent.activePlatform) ?
            retrieveOrdersComponent.ordersFromServer[platformComponent.activePlatform] : null;
        #endregion

        #region Update existing order status 
        orderPagesComponent.childOrderPageComponents.ForEach(orderPageComponent =>
        {
            if (ordersFromServer != null)
            {
                if (ordersFromServer.ContainsKey(orderPageComponent.orderId))
                {
                    orderPageComponent.orderStatus = ordersFromServer[orderPageComponent.orderId].status;
                    orderPageComponent.orderStatusError = ordersFromServer[orderPageComponent.orderId].statusError;
                }
            }
        });
        #endregion
    }
}