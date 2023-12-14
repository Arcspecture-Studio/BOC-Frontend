using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UndoButtonScaleSystem : MonoBehaviour
{
    OrderPagesComponent orderPagesComponent;
    Button button;

    Tween delayedCallTween = null;
    long deletedOrders = 0;
    void Start()
    {
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;

        orderPagesComponent.undoButtonRect.DOScale(0f, 0f);

        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            KillDelayedCallTween();
            delayedCallTween = DOVirtual.DelayedCall(orderPagesComponent.pageDeletedDelaySeconds, () =>
            {
                DestroyOrders();
            });
        });
    }
    void Update()
    {
        if (deletedOrders == orderPagesComponent.orderPagesDeletedParent.childCount) return;
        if (orderPagesComponent.orderPagesDeletedParent.childCount > deletedOrders)
        {
            KillDelayedCallTween();
            orderPagesComponent.undoButtonRect.DOScale(1f, orderPagesComponent.pageAnimDuration).SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    delayedCallTween = DOVirtual.DelayedCall(orderPagesComponent.pageDeletedDelaySeconds, () =>
                    {
                        DestroyOrders();
                    });
                });
        }
        else if (orderPagesComponent.orderPagesDeletedParent.childCount == 0)
        {
            KillDelayedCallTween();
            orderPagesComponent.undoButtonRect.DOScale(0f, orderPagesComponent.pageAnimDuration).SetEase(Ease.InBack);
        }
        deletedOrders = orderPagesComponent.orderPagesDeletedParent.childCount;
    }
    void KillDelayedCallTween()
    {
        if (delayedCallTween != null)
        {
            delayedCallTween.Kill();
        }
    }
    void DestroyOrders()
    {
        orderPagesComponent.undoButtonRect.DOScale(0f, orderPagesComponent.pageAnimDuration).SetEase(Ease.InBack);
        for (int i = 0; i < orderPagesComponent.orderPagesDeletedParent.childCount; i++)
        {
            Destroy(orderPagesComponent.orderPagesDeletedParent.GetChild(i).gameObject);
        }
    }
}
