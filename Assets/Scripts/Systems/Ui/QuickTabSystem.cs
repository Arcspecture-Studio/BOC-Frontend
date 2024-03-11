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
    int spawnedQuickOrderObjectCount = -1;

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
        MovePage();
        UpdateOrderToServer();
        SpawnOrDestroyQuickOrderObject();
        ShowAndHideQuickOrdersObject();
    }

    void InitializeButtonListener()
    {
        quickTabComponent.longButton.onClick.AddListener(() =>
        {
            quickTabComponent.isLong = true;
            AfterClickLongShortButton();
        });
        quickTabComponent.shortButton.onClick.AddListener(() =>
        {
            quickTabComponent.isLong = false;
            AfterClickLongShortButton();
        });
        quickTabComponent.clearEntryPriceButton.onClick.AddListener(() =>
        {
            quickTabComponent.entryPriceInput.text = "";
        });
    }
    void AfterClickLongShortButton()
    {
        quickTabComponent.saveToServer = true;
        quickTabComponent.longButton.interactable = false;
        quickTabComponent.shortButton.interactable = false;
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
    void MovePage()
    {
        if (active == quickTabComponent.active) return;
        if (tween != null)
        {
            if (tween.IsPlaying()) return;
        }
        active = quickTabComponent.active;
        float initialValue = active.Value ? quickTabComponent.inactiveYPosition : quickTabComponent.activeYPosition;
        float moveValue = active.Value ? quickTabComponent.inactiveToActiveYMovement : quickTabComponent.activeToInactiveYMovement;
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
        if (quickTabComponent.spawnedQuickOrderObjects.ContainsKey(orderId)) return;
        GameObject quickOrderDataRowObject = Instantiate(quickTabComponent.quickOrderDataRowPrefab);
        quickOrderDataRowObject.transform.SetParent(quickTabComponent.orderInfoTransform, false);
        QuickOrderDataRowComponent quickOrderDataRowComponent = quickOrderDataRowObject.GetComponent<QuickOrderDataRowComponent>();
        quickOrderDataRowComponent.orderId = orderId;
        quickOrderDataRowComponent.data = orderData;
        quickTabComponent.spawnedQuickOrderObjects.TryAdd(orderId, quickOrderDataRowObject);
    }
    void SpawnOrDestroyQuickOrderObject()
    {
        string spawnQuickOrderString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.SPAWN_QUICK_ORDER);
        if (spawnQuickOrderString.IsNullOrEmpty()) return;
        General.WebsocketSpawnQuickOrderResponse response = JsonConvert.DeserializeObject<General.WebsocketSpawnQuickOrderResponse>(spawnQuickOrderString, JsonSerializerConfig.settings);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.SPAWN_QUICK_ORDER);
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
            quickTabComponent.longButton.interactable = true;
            quickTabComponent.shortButton.interactable = true;
        }
    }
    void ShowAndHideQuickOrdersObject()
    {
        if (spawnedQuickOrderObjectCount == quickTabComponent.spawnedQuickOrderObjects.Count) return;
        spawnedQuickOrderObjectCount = quickTabComponent.spawnedQuickOrderObjects.Count;
        quickTabComponent.quickOrdersObject.SetActive(spawnedQuickOrderObjectCount > 0);
    }
}