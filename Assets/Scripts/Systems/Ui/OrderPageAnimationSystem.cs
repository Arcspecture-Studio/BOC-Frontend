using DG.Tweening;
using UnityEngine;

public class OrderPageAnimationSystem : MonoBehaviour
{
    OrderPagesComponent orderPagesComponent;
    OrderPageComponent orderPageComponent;
    RectTransform rectTransform;

    bool playingDestroySelfAnimation;

    void Start()
    {
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;
        orderPageComponent = GetComponent<OrderPageComponent>();
        rectTransform = GetComponent<RectTransform>();

        Spawn();
        playingDestroySelfAnimation = false;
    }
    void Update()
    {
        Destroy();
    }

    void Spawn()
    {
        orderPageComponent.spawnTween = rectTransform.DOScale(0, 0).OnComplete(() =>
        {
            rectTransform.DOScale(1, orderPagesComponent.pageAnimDuration).SetEase(orderPagesComponent.pageScaleEase);
        });
    }
    void Destroy()
    {
        if (!orderPageComponent.destroySelf) return;
        orderPageComponent.destroySelf = false;
        if (playingDestroySelfAnimation) return;
        playingDestroySelfAnimation = true;
        rectTransform.DOScale(0, orderPagesComponent.pageAnimDuration).SetEase(orderPagesComponent.pageScaleEase).OnComplete(() =>
        {
            playingDestroySelfAnimation = false;

            rectTransform.SetParent(orderPagesComponent.orderPagesDeletedParent);
            orderPageComponent.deleteFromServer = true;
        });
    }
}
