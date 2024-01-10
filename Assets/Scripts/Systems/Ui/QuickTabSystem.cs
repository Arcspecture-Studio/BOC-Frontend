using DG.Tweening;
using UnityEngine;

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

        InitializeButtonListener();
    }
    void Update()
    {
        MoveSettingPage();
        UpdateOrderToServer();
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
               double.Parse(quickTabComponent.entryPriceInput.text),
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
}