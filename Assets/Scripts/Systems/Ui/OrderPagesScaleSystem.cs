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
        UpdateScale();
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
        return orderPagesComponent.childRectTransforms == null
            || orderPagesComponent.childRectTransforms.Count == 0
            || orderPagesComponent.transform.childCount != orderPagesComponent.childRectTransforms.Count;
    }
    void Scale()
    {
        for (int i = 0; i < orderPagesComponent.childRectTransforms.Count; i++)
        {
            if (orderPagesComponent.childRectTransforms[i] == null) continue;
            if (orderPagesComponent.childOrderPageComponents[i].spawnTween.IsActive())
            {
                orderPagesComponent.childOrderPageComponents[i].spawnTween.Complete();
            }
            switch (status)
            {
                case OrderPagesStatusEnum.IMMERSIVE:
                    if (orderPagesComponent.currentPageIndex == i)
                    {
                        orderPagesComponent.childRectTransforms[i].DOScale(1f, orderPagesComponent.pageAnimDuration).SetEase(orderPagesComponent.pageScaleEase);
                    }
                    else
                    {
                        orderPagesComponent.childRectTransforms[i].DOScale(0f, orderPagesComponent.pageAnimDuration).SetEase(orderPagesComponent.pageScaleEase);
                    }
                    break;
                case OrderPagesStatusEnum.DETACH:
                    orderPagesComponent.childRectTransforms[i].DOScale(orderPagesComponent.pageScaleTarget, orderPagesComponent.pageAnimDuration).SetEase(orderPagesComponent.pageScaleEase);
                    break;
            }
        }
    }
}
