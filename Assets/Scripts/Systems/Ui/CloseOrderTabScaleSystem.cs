using DG.Tweening;
using UnityEngine;

public class CloseOrderTabScaleSystem : MonoBehaviour
{
    OrderPagesComponent orderPagesComponent;

    OrderPagesStatusEnum? status;
    int? childCount;

    void Start()
    {
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;

        status = null;
        childCount = null;
    }
    void Update()
    {
        if (status == orderPagesComponent.status && childCount == orderPagesComponent.transform.childCount) return;
        status = orderPagesComponent.status;
        childCount = orderPagesComponent.transform.childCount;
        switch (status)
        {
            case OrderPagesStatusEnum.IMMERSIVE:
                orderPagesComponent.closeTabButtonRect.DOScale(0f, orderPagesComponent.pageAnimDuration).SetEase(Ease.InBack);
                break;
            case OrderPagesStatusEnum.DETACH:
                if (orderPagesComponent.transform.childCount > 0)
                {
                    orderPagesComponent.closeTabButtonRect.DOScale(1f, orderPagesComponent.pageAnimDuration).SetEase(Ease.OutBack);
                }
                else
                {
                    orderPagesComponent.closeTabButtonRect.DOScale(0f, orderPagesComponent.pageAnimDuration).SetEase(Ease.InBack);
                }
                break;
        }
    }
}
