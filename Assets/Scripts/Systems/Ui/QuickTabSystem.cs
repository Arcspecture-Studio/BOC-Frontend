using System.Collections.Generic;
using DG.Tweening;
using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class QuickTabSystem : MonoBehaviour
{
    QuickTabComponent quickTabComponent;
    WebsocketComponent websocketComponent;
    PlatformComponent platformComponent;
    PreferenceComponent preferenceComponent;

    bool? active = null;
    Tween tween = null;

    void Start()
    {
        quickTabComponent = GlobalComponent.instance.quickTabComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        preferenceComponent = GlobalComponent.instance.preferenceComponent;

        quickTabComponent.onChange_quickOrdersFromServer.AddListener(OnChange_QuickOrdersFromServer);
        InitializeButtonListener();
    }
    void Update()
    {
        MoveSettingPage();
        UpdateOrderToServer();
        SpawnOrDestroyQuickOrderObject();
    }

    void InitializeButtonListener()
    {
        quickTabComponent.longButton.onClick.AddListener(() =>
        {
            quickTabComponent.saveToServer = true;
            quickTabComponent.isLong = true;
        });
        quickTabComponent.shortButton.onClick.AddListener(() =>
        {
            quickTabComponent.saveToServer = true;
            quickTabComponent.isLong = false;
        });
    }
    void UpdateOrderToServer()
    {
        if (quickTabComponent.saveToServer)
        {
            quickTabComponent.saveToServer = false;
            double normalizedMarginWeightDistributionValue = preferenceComponent.marginWeightDistributionValue * OrderConfig.MARGIN_WEIGHT_DISTRIBUTION_RANGE;
            double entryPrice = quickTabComponent.entryPriceInput.text.IsNullOrEmpty() ? -1 : double.Parse(quickTabComponent.entryPriceInput.text);
            websocketComponent.generalRequests.Add(new General.WebsocketSaveQuickOrderRequest(
               platformComponent.tradingPlatform,
               preferenceComponent.symbol,
               preferenceComponent.lossPercentage,
               preferenceComponent.lossAmount,
               preferenceComponent.marginDistributionMode == MarginDistributionModeEnum.WEIGHTED,
               normalizedMarginWeightDistributionValue,
               preferenceComponent.takeProfitType,
               preferenceComponent.riskRewardRatio,
               preferenceComponent.takeProfitTrailingCallbackPercentage,
               entryPrice,
               int.Parse(quickTabComponent.entryTimesInput.text),
               WebsocketIntervalEnum.array[quickTabComponent.atrTimeframeDropdown.value],
               int.Parse(quickTabComponent.atrLengthInput.text),
               double.Parse(quickTabComponent.atrMultiplierInput.text),
               quickTabComponent.isLong
           ));
        }
    }
    void MoveSettingPage()
    {
        if (active == quickTabComponent.active) return;
        if (tween != null)
        {
            if (tween.IsPlaying()) return;
        }
        active = quickTabComponent.active;
        float initialValue = active.Value ? -200 : 200;
        float moveValue = active.Value ? 400f : -400f;
        quickTabComponent.rectTransform.anchoredPosition = new Vector2(quickTabComponent.rectTransform.anchoredPosition.x,
            initialValue);
        tween = quickTabComponent.rectTransform.DOBlendableLocalMoveBy(new Vector3(0, moveValue, 0), quickTabComponent.pageMoveDuration).SetEase(quickTabComponent.pageMoveEase);
    }
    void OnChange_QuickOrdersFromServer(Dictionary<string, General.WebsocketRetrieveQuickOrdersData> quickOrdersFromServer)
    {
        if (quickOrdersFromServer == null) return;

        #region Instantiate orders
        foreach (KeyValuePair<string, General.WebsocketRetrieveQuickOrdersData> order in quickOrdersFromServer)
        {
            InstantiateQuickOrder(order.Key, order.Value);
        }
        #endregion
    }
    void InstantiateQuickOrder(string orderId, General.WebsocketRetrieveQuickOrdersData orderData)
    {
        GameObject quickOrderDataRowObject = Instantiate(quickTabComponent.quickOrderDataRowPrefab);
        quickOrderDataRowObject.transform.SetParent(quickTabComponent.orderInfoTransform, false);
        QuickOrderDataRowComponent quickOrderDataRowComponent = quickOrderDataRowObject.GetComponent<QuickOrderDataRowComponent>();
        quickOrderDataRowComponent.orderId = orderId;
        quickOrderDataRowComponent.data = orderData;
        if (!quickTabComponent.spawnedQuickOrderObjects.TryAdd(orderId, quickOrderDataRowObject))
            quickTabComponent.spawnedQuickOrderObjects[orderId] = quickOrderDataRowObject;
    }
    void SpawnOrDestroyQuickOrderObject() // TODO: test this
    {
        string spawnQuickOrderString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.SPAWN_QUICK_ORDER.ToString());
        if (spawnQuickOrderString.IsNullOrEmpty()) return;
        General.WebsocketSpawnQuickOrderResponse response = JsonConvert.DeserializeObject<General.WebsocketSpawnQuickOrderResponse>(spawnQuickOrderString, JsonSerializerConfig.settings);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.SPAWN_QUICK_ORDER.ToString());
        if (response.quickOrder == null) // destroy
        {
            GameObject quickOrderObject;
            if (quickTabComponent.spawnedQuickOrderObjects.TryGetValue(response.orderId, out quickOrderObject))
            {
                Destroy(quickOrderObject);
                quickTabComponent.spawnedQuickOrderObjects.Remove(response.orderId);
            }
        }
        else // spawn
        {
            InstantiateQuickOrder(response.orderId, response.quickOrder);
        }
    }
}