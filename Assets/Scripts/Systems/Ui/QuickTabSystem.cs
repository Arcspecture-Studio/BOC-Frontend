using DG.Tweening;
using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class QuickTabSystem : MonoBehaviour
{
    QuickTabComponent quickTabComponent;
    WebsocketComponent websocketComponent;
    PlatformComponent platformComponent;
    ProfileComponent profileComponent;
    SpawnQuickOrderComponent spawnQuickOrderComponent;
    LoginComponent loginComponent;

    bool? active = null;
    Tween tween = null;
    int spawnedQuickOrderObjectCount = -1;

    void Start()
    {
        quickTabComponent = GlobalComponent.instance.quickTabComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        profileComponent = GlobalComponent.instance.profileComponent;
        spawnQuickOrderComponent = GlobalComponent.instance.spawnQuickOrderComponent;
        loginComponent = GlobalComponent.instance.loginComponent;

        quickTabComponent.onChange_addToServer.AddListener(AddToServer);
        quickTabComponent.onChange_deleteFromServer.AddListener(DeleteFromServer);

        InitializeButtonListener();
    }
    void Update()
    {
        MovePage();
        SpawnQuickOrderObject();
        DestroyQuickOrderObject();
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
        quickTabComponent.addToServer = true;
        quickTabComponent.longButton.interactable = false;
        quickTabComponent.shortButton.interactable = false;
    }
    void AddToServer()
    {
        ProfilePerference preference = profileComponent.activeProfile.preference;
        double entryPrice = quickTabComponent.entryPriceInput.text.IsNullOrEmpty() ? -1 : double.Parse(quickTabComponent.entryPriceInput.text);
        websocketComponent.generalRequests.Add(new General.WebsocketAddQuickOrderRequest(
            loginComponent.token,
            platformComponent.activePlatform,
            preference.symbol,
            preference.lossPercentage,
            preference.lossAmount,
            preference.marginDistributionMode == MarginDistributionModeEnum.WEIGHTED,
            preference.marginWeightDistributionValue,
            preference.takeProfitType,
            preference.orderType,
            preference.riskRewardRatio,
            preference.takeProfitTrailingCallbackPercentage,
            entryPrice,
            int.Parse(quickTabComponent.entryTimesInput.text),
            (TimeframeEnum)quickTabComponent.atrTimeframeDropdown.value,
            int.Parse(quickTabComponent.atrLengthInput.text),
            double.Parse(quickTabComponent.atrMultiplierInput.text),
            quickTabComponent.isLong
        ));
    }
    void DeleteFromServer(string orderId)
    {
        websocketComponent.generalRequests.Add(new General.WebsocketDeleteQuickOrderRequest(loginComponent.token, orderId));
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
    void SpawnQuickOrderObject()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.ADD_QUICK_ORDER);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.ADD_QUICK_ORDER);
        if (jsonString.IsNullOrEmpty()) return;

        General.WebsocketAddQuickOrderResponse response = JsonConvert.DeserializeObject<General.WebsocketAddQuickOrderResponse>(jsonString, JsonSerializerConfig.settings);

        quickTabComponent.longButton.interactable = true;
        quickTabComponent.shortButton.interactable = true;
        spawnQuickOrderComponent.quickOrderToSpawn = response.quickOrder;
    }
    void DestroyQuickOrderObject()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.DELETE_QUICK_ORDER);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.DELETE_QUICK_ORDER);
        if (jsonString.IsNullOrEmpty()) return;

        General.WebsocketDeleteQuickOrderResponse response = JsonConvert.DeserializeObject<General.WebsocketDeleteQuickOrderResponse>(jsonString, JsonSerializerConfig.settings);

        if (quickTabComponent.spawnedQuickOrderObjects.TryGetValue(response.orderId, out GameObject quickOrderObject))
        {
            Destroy(quickOrderObject);
            quickTabComponent.spawnedQuickOrderObjects.Remove(response.orderId);
        }
    }
    void ShowAndHideQuickOrdersObject()
    {
        if (spawnedQuickOrderObjectCount == quickTabComponent.spawnedQuickOrderObjects.Count) return;
        spawnedQuickOrderObjectCount = quickTabComponent.spawnedQuickOrderObjects.Count;
        quickTabComponent.quickOrdersObject.SetActive(spawnedQuickOrderObjectCount > 0);
    }
}