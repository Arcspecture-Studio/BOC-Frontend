using DG.Tweening;
using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class TradingBotTabSystem : MonoBehaviour
{
    TradingBotTabComponent tradingBotTabComponent;

    bool? active = null;
    Tween tween = null;
    int spawnedTradingBotObjectCount = -1;

    void Start()
    {
        tradingBotTabComponent = GlobalComponent.instance.tradingBotTabComponent;
    }
    void Update()
    {
        MovePage();
        ShowAndHideQuickOrdersObject();
    }

    void MovePage()
    {
        if (active == tradingBotTabComponent.active) return;
        if (tween != null && tween.IsPlaying()) return;

        active = tradingBotTabComponent.active;
        float initialValue = active.Value ? tradingBotTabComponent.inactiveYPosition : tradingBotTabComponent.activeYPosition;
        float moveValue = active.Value ? tradingBotTabComponent.inactiveToActiveYMovement : tradingBotTabComponent.activeToInactiveYMovement;
        tradingBotTabComponent.rectTransform.anchoredPosition = new Vector2(tradingBotTabComponent.rectTransform.anchoredPosition.x,
            initialValue);
        tween = tradingBotTabComponent.rectTransform.DOBlendableLocalMoveBy(new Vector3(0, moveValue, 0), tradingBotTabComponent.pageMoveDuration).SetEase(tradingBotTabComponent.pageMoveEase);
    }
    void ShowAndHideQuickOrdersObject()
    {
        if (spawnedTradingBotObjectCount == tradingBotTabComponent.spawnedTradingBotObjects.Count) return;
        spawnedTradingBotObjectCount = tradingBotTabComponent.spawnedTradingBotObjects.Count;
        tradingBotTabComponent.tradingBotListObject.SetActive(spawnedTradingBotObjectCount > 0);
    }
}