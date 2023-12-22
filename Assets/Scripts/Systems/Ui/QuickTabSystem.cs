using DG.Tweening;
using UnityEngine;

public class QuickTabSystem : MonoBehaviour
{
    QuickTabComponent quickTabComponent;

    bool? active = null;
    Tween tween = null;

    void Start()
    {
        quickTabComponent = GlobalComponent.instance.quickTabComponent;
    }
    void Update()
    {
        MoveSettingPage();
    }

    void MoveSettingPage()
    {
        if (active == quickTabComponent.active) return;
        if (tween != null)
        {
            if (tween.IsPlaying()) return;
        }
        active = quickTabComponent.active;
        float initialValue = active.Value ? -250 : 250;
        float moveValue = active.Value ? 500f : -500f;
        quickTabComponent.rectTransform.anchoredPosition = new Vector2(quickTabComponent.rectTransform.anchoredPosition.x,
            initialValue);
        tween = quickTabComponent.rectTransform.DOBlendableLocalMoveBy(new Vector3(0, moveValue, 0), quickTabComponent.pageMoveDuration).SetEase(quickTabComponent.pageMoveEase);
    }
}