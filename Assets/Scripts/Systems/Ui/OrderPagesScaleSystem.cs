using DG.Tweening;
using UnityEngine;

public class OrderPagesScaleSystem : MonoBehaviour
{
    OrderPagesComponent orderPagesComponent;

    OrderPagesStatusEnum? status;
    long? childCount;

    void Start()
    {
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;

        status = null;
        childCount = null;
    }

    void Update()
    {
        UpdateScaleWhenStatusAndChildCountChanged();
        // UpdateScale();
    }
    void UpdateScaleWhenStatusAndChildCountChanged()
    {
        if (isChildRectTransformsNotReady()) return;
        if (status == orderPagesComponent.status && childCount == orderPagesComponent.transform.childCount) return;
        status = orderPagesComponent.status;
        childCount = orderPagesComponent.transform.childCount;
        Scale();
    }
    void UpdateScale()
    {
        if (isChildRectTransformsNotReady()) return;
        if (!orderPagesComponent.scaleOrders) return;
        orderPagesComponent.scaleOrders = false;
        Scale();
    }
    bool isChildRectTransformsNotReady()
    {
        if (orderPagesComponent.childOrderPageComponents.Count == 0) childCount = 0;
        return orderPagesComponent.childOrderPageComponents == null
            || orderPagesComponent.childOrderPageComponents.Count == 0
            || orderPagesComponent.transform.childCount != orderPagesComponent.childOrderPageComponents.Count;
    }
    void Scale()
    {
        for (int i = 0; i < orderPagesComponent.childOrderPageComponents.Count; i++)
        {
            OrderPageComponent orderPageComponent = orderPagesComponent.childOrderPageComponents[i];
            if (orderPageComponent == null) continue;
            if (orderPageComponent.spawnTween.IsActive())
            {
                orderPageComponent.spawnTween.Complete();
            }
            switch (status)
            {
                case OrderPagesStatusEnum.IMMERSIVE:
                    if (orderPagesComponent.currentPageIndex == i)
                    {
                        orderPageComponent.rectTransform.DOScale(1f, orderPagesComponent.pageAnimDuration)
                        .SetEase(orderPagesComponent.pageScaleEase);
                    }
                    else
                    {
                        orderPageComponent.scrollRectYPos = orderPageComponent.scrollRect.normalizedPosition.y;
                        orderPageComponent.rectTransform.DOScale(0f, orderPagesComponent.pageAnimDuration)
                        .SetEase(orderPagesComponent.pageScaleEase);
                    }
                    break;
                case OrderPagesStatusEnum.DETACH:
                    bool runCallback = orderPagesComponent.currentPageIndex != i;
                    orderPageComponent.rectTransform.DOScale(orderPagesComponent.pageScaleTarget, orderPagesComponent.pageAnimDuration)
                    .SetEase(orderPagesComponent.pageScaleEase)
                    .OnComplete(() =>
                    {
                        if (runCallback)
                        {
                            orderPageComponent.scrollRect.normalizedPosition = new Vector2(
                                orderPageComponent.scrollRect.normalizedPosition.x,
                                orderPageComponent.scrollRectYPos
                            );
                        }
                    });
                    break;
            }
        }
    }
}
